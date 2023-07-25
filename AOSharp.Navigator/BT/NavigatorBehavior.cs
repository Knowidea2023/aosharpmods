using AOSharp.Core.Movement;
using AOSharp.Core;
using BehaviourTree.FluentBuilder;
using BehaviourTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOSharp.Common.GameData;
using AOSharp.Core.UI;
using AOSharp.Pathfinding;
using org.critterai.nav;
using System.IO;

namespace AOSharp.Navigator.BT
{
    internal class NavigatorBehavior
    {
        internal static IBehaviour<NavigatorContext> NavBehavior()
        {
            return FluentBuilder.Create<NavigatorContext>()
                .Sequence("Root")
                    .Subtree(NavigateTasks())
                .End()
                .Build();
        }

        internal static IBehaviour<NavigatorContext> NavigateTasks()
        {
            return FluentBuilder.Create<NavigatorContext>()
                .Sequence("Navigate Tasks")
                    .Condition("Are there any tasks?", c => c.Tasks.Any())
                    .Do("Load Navmesh", LoadNavmesh)
                    .Selector("Transverse Link")
                        .Subtree(TransverseTerminalLink())
                        .Subtree(TransverseTeleporterLink())
                        .Subtree(TransverseZoneBorderLink())
                        .Subtree(MoveToDestination())
                    .End()
                .End()
                .Build();
        }

        internal static IBehaviour<NavigatorContext> TransverseTerminalLink()
        {
            TerminalLink terminalLink = null;

            return FluentBuilder.Create<NavigatorContext>()
                .Sequence("Transverse Terminal Link")
                    .Condition("Is Terminal Link?", c => TryConvertTask(c.Tasks.Peek(), out terminalLink))
                    .Do("Move to Terminal", c => MoveToTransitionSpot(c, terminalLink, terminalLink.TerminalPos))
                    .Do("Use Terminal", c => UseTerminal(c, terminalLink))
                .End()
                .Build();
        }

        internal static IBehaviour<NavigatorContext> TransverseTeleporterLink()
        {
            TeleporterLink teleporterLink = null;

            return FluentBuilder.Create<NavigatorContext>()
                .Sequence("Transverse Teleporter Link")
                    .Condition("Is Teleporter Link?", c => TryConvertTask(c.Tasks.Peek(), out teleporterLink))
                    .Do("Move to teleporter", c => MoveToTransitionSpot(c, teleporterLink, teleporterLink.TeleporterPos))
                .End()
                .Build();
        }

        internal static IBehaviour<NavigatorContext> TransverseZoneBorderLink()
        {
            ZoneBorderLink zoneBorderLink = null;

            return FluentBuilder.Create<NavigatorContext>()
                .Sequence("Transverse Zone Border Link")
                    .Condition("Is Zone Border Link?", c => TryConvertTask(c.Tasks.Peek(), out zoneBorderLink))
                    .Do("Move to zone border", c => MoveToTransitionSpot(c, zoneBorderLink, zoneBorderLink.TransitionSpots[0]))
                .End()
                .Build();
        }

        internal static IBehaviour<NavigatorContext> MoveToDestination()
        {
            MoveToTask moveTask = null;

            return FluentBuilder.Create<NavigatorContext>()
                .Sequence("Move To Destination")
                    .Condition("Is MoveTo Task?", c => TryConvertTask(c.Tasks.Peek(), out moveTask))
                    .Do("Move to destination", c => MoveToTransitionSpot(c, moveTask, moveTask.Destination))
                .End()
                .Build();
        }

        public static BehaviourStatus LoadNavmesh(NavigatorContext context)
        {
            if (!(MovementController.Instance is NewNavmeshMovementController movementController))
            {
                movementController = new NewNavmeshMovementController();
                MovementController.Set(movementController);
            }

            if(!context.NavmeshCache.TryGetValue((PlayfieldId)Playfield.ModelIdentity.Instance, out Navmesh navmesh))
            {
                string navMeshPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\NavMeshes\\{Playfield.ModelIdentity.Instance}.Navmesh";

                if (!File.Exists(navMeshPath) || !movementController.LoadNavmesh(navMeshPath, out navmesh))
                {
                    //TODO: Throw exception
                    Chat.WriteLine($"Unable to load Nav mesh for {Playfield.ModelIdentity.Instance}");
                    Stop(context);
                    return BehaviourStatus.Failed;
                }

                context.NavmeshCache.Add((PlayfieldId)Playfield.ModelIdentity.Instance, navmesh);
            }

            if (movementController.Pathfinder == null || !movementController.Pathfinder.IsUsingNavmesh(navmesh))
            {
                Chat.WriteLine($"Loading navmesh for {Playfield.ModelIdentity.Instance}");
                movementController.SetNavmesh(navmesh);
            }

            return BehaviourStatus.Succeeded;
        }

        public static BehaviourStatus MoveToTransitionSpot(NavigatorContext context, NavigatorTask task, Vector3 transitionPos)
        {
            if (!IsTaskValid(context, task))
                return BehaviourStatus.Succeeded;

            NewNavmeshMovementController movementController = MovementController.Instance as NewNavmeshMovementController;

            if (Vector3.Distance(DynelManager.LocalPlayer.Position, transitionPos) < 1f && !movementController.IsNavigating)
            {
                if (task is MoveToTask)
                {
                    context.Tasks.Dequeue();

                    if (!context.Tasks.Any())
                        context.Navigator.DestinationReachedCallback?.Invoke();
                }

                return BehaviourStatus.Succeeded;
            }

            if (movementController.IsNavigating)
                return BehaviourStatus.Running;

            movementController.SetNavMeshDestination(transitionPos);
            return BehaviourStatus.Running;
        }

        public static BehaviourStatus UseTerminal (NavigatorContext context, TerminalLink terminalLink)
        {
            if (DynelManager.Find(terminalLink.TerminalName, out SimpleItem terminal))
            {
                terminal.Use();
                return BehaviourStatus.Succeeded;
            }
            else
            {
                return BehaviourStatus.Failed;
            }
        }

        public static void Stop(NavigatorContext context)
        {
            context.Tasks.Clear();
        }

        private static bool IsTaskValid(NavigatorContext context, NavigatorTask task)
        {
            return context.Tasks.Contains(task);
        }

        public static bool TryConvertTask<T>(NavigatorTask task, out T convertedTask) where T : NavigatorTask
        {
            convertedTask = task as T;
            return task is T;
        }
    }
}
