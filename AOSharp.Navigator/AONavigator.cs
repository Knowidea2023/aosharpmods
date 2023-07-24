using AOSharp.Common.GameData;
using AOSharp.Core;
using AOSharp.Core.Misc;
using AOSharp.Core.Movement;
using AOSharp.Core.UI;
using AOSharp.Navigator.BT;
using BehaviourTree;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Navigator
{
    public class AONavigator
    {
        public Dictionary<PlayfieldId, PlayfieldNode> PlayfieldMap;
        private NavigatorContext _btContext;
        public IBehaviour<NavigatorContext> Behaviour;
        private AutoResetInterval _internalTick = new AutoResetInterval(100);

        public AONavigator() 
        {
            InitPlayfields();

            _btContext = new NavigatorContext();
            Behaviour = NavigatorBehavior.NavBehavior();

            Game.TeleportEnded += TeleportEnded;
        }

        private void TeleportEnded(object sender, object e)
        {
            if (!_btContext.Tasks.Any())
                return;

            NavigatorTask task = _btContext.Tasks.Dequeue();

            //Chat.WriteLine($"Dequeuing task: {task} - {task.DstId}");
            //Chat.WriteLine($"Next Task task: {_btContext.Tasks.Peek()} - {_btContext.Tasks.Peek().DstId}");

            if (task.DstId != (PlayfieldId)Playfield.ModelIdentity.Instance)
            {
                _btContext.Tasks.Clear();
                return;
            }
        }

        public void Update()
        {
            if (Game.IsZoning)
                return;

            if (!_internalTick.Elapsed)
                return;

            Behaviour.Tick(_btContext);
        }

        public void MoveTo(PlayfieldId id, Vector3 pos)
        {
            if((PlayfieldId)Playfield.ModelIdentity.Instance != id)
            {
                var path = GetPathTo(id);

                if (path.Count == 0)
                {
                    //TODO Throw no path
                    Chat.WriteLine($"No path to {id}");
                    return;
                }

                foreach (PlayfieldLink link in path)
                {
                    _btContext.Tasks.Enqueue(link);
                }
            }

            _btContext.Tasks.Enqueue(new MoveToTask(id, pos));
        }

        public List<PlayfieldLink> GetPathTo(PlayfieldId toId)
        {
            return GetPathFromTo((PlayfieldId)Playfield.ModelIdentity.Instance, toId);
        }

        public List<PlayfieldLink> GetPathFromTo(PlayfieldId fromId, PlayfieldId toId)
        {
            List<PlayfieldId> path = new List<PlayfieldId>();
            List<PlayfieldId> visited = new List<PlayfieldId>();
            Queue<PlayfieldId> queue = new Queue<PlayfieldId>();

            visited.Add(fromId);
            queue.Enqueue(fromId);

            while(queue.Any())
            {
                PlayfieldId id = queue.Dequeue();

                path.Add(id);

                if (id == toId)
                    break;

                foreach(PlayfieldLink link in PlayfieldMap[id].Links)
                {
                    if (visited.Contains(link.DstId))
                        continue;

                    visited.Add(link.DstId);
                    queue.Enqueue(link.DstId);
                }
            }

            path.Reverse();

            List<PlayfieldLink> pathLinks = new List<PlayfieldLink>();
            PlayfieldId lastValidId = toId;

            foreach (PlayfieldId id in path)
            {
                if (PlayfieldMap[id].TryGetLink(lastValidId, out PlayfieldLink link))
                { 
                    pathLinks.Add(link);
                    lastValidId = id;
                }
            }

            pathLinks.Reverse();

            return pathLinks;
        }

        private void InitPlayfields()
        {
            string configPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\PlayfieldLinks.json";
            
            if (!File.Exists(configPath))
                return;

            try
            {
                PlayfieldMap = JsonConvert.DeserializeObject<Dictionary<PlayfieldId, PlayfieldNode>>(File.ReadAllText(configPath));
            }
            catch (Exception e)
            {
                Chat.WriteLine($"Failed to load PlayfieldLinks.json. The error message was: {e}");
            }
        }
    }
}
