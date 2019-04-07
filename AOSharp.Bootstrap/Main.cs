﻿using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;
using EasyHook;
using AOSharp.Bootstrap.IPC;

namespace AOSharp.Bootstrap
{
    [Serializable]
    public class Main : IEntryPoint
    {
        private IPCServer _ipcPipe;
        private AppDomain _pluginAppDomain;
        private ManualResetEvent _connectEvent;
        private ManualResetEvent _unloadEvent;
        private static List<LocalHook> _hooks = new List<LocalHook>();
        private PluginProxy _pluginProxy;

        public Main(RemoteHooking.IContext inContext, String inChannelName)
        {
            _connectEvent = new ManualResetEvent(false);
            _unloadEvent = new ManualResetEvent(false);

            //Setup IPC server that will be used for handling API requests and events.
            _ipcPipe = new IPCServer(inChannelName);
            _ipcPipe.OnConnected = OnIPCClientConnected;
            _ipcPipe.OnDisconnected = OnIPCClientDisconnected;
            _ipcPipe.RegisterCallback((byte)HookOpCode.LoadAssembly, typeof(LoadAssemblyMessage), OnAssembliesChanged);
            _ipcPipe.Start();
        }

        public void Run(RemoteHooking.IContext inContext, String inChannelName)
        {
            //If the GameController doesn't connect within 10 seconds we will unload the dll.
            if (!_connectEvent.WaitOne(10000))
                return;

            //Wait for the signal to unload the dll.
            _unloadEvent.WaitOne();
        }

        private void OnIPCClientConnected(IPCServer pipe)
        {
            //Notify the main thread we recieved a connection from the GameController.
            _connectEvent.Set();

            SetupHooks();
        }

        private void OnIPCClientDisconnected(IPCServer pipe)
        {
            UnhookAll();

            _ipcPipe.Close();

            if (_pluginAppDomain != null)
            {
                AppDomain.Unload(_pluginAppDomain);
                _pluginAppDomain = null;
            }

            //Notify the main thread that it is time to unload the dll.
            _unloadEvent.Set();
        }

        private void OnAssembliesChanged(object pipe, IPCMessage message)
        {
            LoadAssemblyMessage msg = message as LoadAssemblyMessage;

            if (_pluginAppDomain != null)
            {
                //Release existing AppDomain
                AppDomain.Unload(_pluginAppDomain);
                _pluginAppDomain = null;
            }

            if (msg.Assemblies.Count == 0)
                return;

            try
            {
                AppDomainSetup setup = new AppDomainSetup()
                {
                    ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
                };

                _pluginAppDomain = AppDomain.CreateDomain("plugins", null, setup);

                Type type = typeof(PluginProxy);
                _pluginProxy = (PluginProxy)_pluginAppDomain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);

                _pluginProxy.LoadCore(_pluginAppDomain.BaseDirectory + "\\AOSharp.Core.dll");

                foreach (string assembly in msg.Assemblies)
                {
                    _pluginProxy.LoadPlugin(assembly);
                }
            }
            catch (Exception e)
            {
                //TODO: Send IPC message back to loader on error
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"AOSharp.Bootstrap_Exception.txt", true))
                {
                    file.WriteLine($"{DateTime.Now}: {e.Message}");
                }
            }
        }

        private void SetupHooks()
        {
            CreateHook("N3.dll",
                        "?AddChildDynel@n3Playfield_t@@QAEXPAVn3Dynel_t@@ABVVector3_t@@ABVQuaternion_t@@@Z",
                        new N3Playfield_t.DAddChildDynel(N3Playfield_t__AddChildDynel_Hook));

            CreateHook("Gamecode.dll",
                        "?RunEngine@n3EngineClientAnarchy_t@@UAEXM@Z",
                        new N3EngineClientAnarchy_t.DRunEngine(N3EngineClientAnarchy_RunEngine_Hook));

            CreateHook("Gamecode.dll",
                        "?N3Msg_SendInPlayMessage@n3EngineClientAnarchy_t@@QBE_NXZ",
                        new N3EngineClientAnarchy_t.DSendInPlayMessage(N3EngineClientAnarchy_SendInPlayMessage_Hook));

            CreateHook("GUI.dll",
                        "?TeleportStartedMessage@FlowControlModule_t@@CAXXZ",
                        new FlowControlModule_t.DTeleportStartedMessage(FlowControlModule_t_TeleportStarted_Hook));

            CreateHook("Gamecode.dll",
                        "?TeleportFailed@TeleportTrier_t@@QAEXXZ",
                        new TeleportTrier_t.DTeleportFailed(TeleportTrier_t_TeleportFailed_Hook));
        }

        private void CreateHook(string module, string funcName, Delegate newFunc)
        {
            CreateHook(LocalHook.GetProcAddress(module, funcName), newFunc);
        }

        private void CreateHook(IntPtr origFunc, Delegate newFunc)
        {
            LocalHook hook = LocalHook.Create(origFunc, newFunc, this);
            hook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
            _hooks.Add(hook);
        }

        private void UnhookAll()
        {
            foreach (LocalHook hook in _hooks)
                hook.Dispose();
        }

        public unsafe void FlowControlModule_t_TeleportStarted_Hook()
        {
            try
            {
                if(!*FlowControlModule_t.pIsTeleporting)
                    _pluginProxy.TeleportStarted();
            }
            catch (Exception) { }

            FlowControlModule_t.TeleportStartedMessage();
        }

        public void TeleportTrier_t_TeleportFailed_Hook(IntPtr pThis)
        {
            try
            {
                _pluginProxy.TeleportFailed();
            }
            catch (Exception) { }

            TeleportTrier_t.TeleportFailed(pThis);
        }

        public bool N3EngineClientAnarchy_SendInPlayMessage_Hook(IntPtr pThis)
        {
            try
            {
                _pluginProxy.TeleportEnded();
            }
            catch (Exception) { }

            return N3EngineClientAnarchy_t.SendInPlayMessage(pThis);
        }

        public void N3EngineClientAnarchy_RunEngine_Hook(IntPtr pThis, float deltaTime)
        {
            try
            {
                _pluginProxy.Update(deltaTime);
            }
            catch (Exception) { }

            N3EngineClientAnarchy_t.RunEngine(pThis, deltaTime);
        }

        public void N3Playfield_t__AddChildDynel_Hook(IntPtr pThis, IntPtr pDynel, IntPtr pos, IntPtr rot)
        {
            //Let the client load the dynel before we notify the GameController of it's spawn.
            N3Playfield_t.AddChildDynel(pThis, pDynel, pos, rot);

            try
            {
                _pluginProxy.DynelSpawned(pDynel);
            }
            catch (Exception) { }
        }

        public class CoreDelegates
        {
            public delegate void DynelSpawnedDelegate(IntPtr pDynel);
            public DynelSpawnedDelegate DynelSpawned;
            public delegate void UpdateDelegate(float deltaTime);
            public UpdateDelegate Update;
            public delegate void TeleportStartedDelegate();
            public TeleportStartedDelegate TeleportStarted;
            public delegate void TeleportEndedDelegate();
            public TeleportEndedDelegate TeleportEnded;
            public delegate void TeleportFailedDelegate();
            public TeleportFailedDelegate TeleportFailed;
        }

        public class PluginProxy : MarshalByRefObject
        {
            private static CoreDelegates _coreDelegates;

            public void DynelSpawned(IntPtr pDynel)
            {
                if(_coreDelegates.DynelSpawned != null)
                    _coreDelegates.DynelSpawned(pDynel);
            }

            public void Update(float deltaTime)
            {
                if (_coreDelegates.Update != null)
                    _coreDelegates.Update(deltaTime);
            }

            public void TeleportStarted()
            {
                if (_coreDelegates.TeleportStarted != null)
                    _coreDelegates.TeleportStarted();
            }

            public void TeleportEnded()
            {
                if (_coreDelegates.TeleportEnded != null)
                    _coreDelegates.TeleportEnded();
            }

            public void TeleportFailed()
            {
                if (_coreDelegates.TeleportFailed != null)
                    _coreDelegates.TeleportFailed();
            }

            private T CreateDelegate<T>(Assembly assembly, string className, string methodName) where T: class
            {
                Type t = assembly.GetType(className);

                if (t == null)
                    return default(T);
                
                MethodInfo m = t.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);

                if (m == null)
                    return default(T);

                return Delegate.CreateDelegate(typeof(T), m) as T;
            }

            public void LoadCore(string assemblyPath)
            {
                //Load main assembly
                var assembly = Assembly.LoadFile(assemblyPath);

                //Load references
                foreach (var reference in assembly.GetReferencedAssemblies())
                {
                    Assembly.Load(reference);
                }

                _coreDelegates = new CoreDelegates()
                {
                    Update = CreateDelegate<CoreDelegates.UpdateDelegate>(assembly, "AOSharp.Core.Game", "OnUpdateInternal"),
                    DynelSpawned = CreateDelegate<CoreDelegates.DynelSpawnedDelegate>(assembly, "AOSharp.Core.DynelManager", "DynelSpawnedInternal"),
                    TeleportStarted = CreateDelegate<CoreDelegates.TeleportStartedDelegate>(assembly, "AOSharp.Core.Game", "OnTeleportStartedInternal"),
                    TeleportEnded = CreateDelegate<CoreDelegates.TeleportEndedDelegate>(assembly, "AOSharp.Core.Game", "OnTeleportEndedInternal"),
                    TeleportFailed = CreateDelegate<CoreDelegates.TeleportFailedDelegate>(assembly, "AOSharp.Core.Game", "OnTeleportFailedInternal")
                };
            }

            public void LoadPlugin(string assemblyPath)
            {
                try
                {
                    //Load main assembly
                    Assembly assembly = Assembly.LoadFile(assemblyPath);

                    //Load references
                    foreach (AssemblyName reference in assembly.GetReferencedAssemblies())
                    {
                        if (reference.Name == "AOSharp.Common" ||
                            reference.Name == "AOSharp.Core")
                            continue;

                        Assembly.Load(reference);
                    }

                    // Find the first AOSharp.Core.IAOPluginEntry
                    Type[] exportedTypes = assembly.GetExportedTypes();
                    foreach(Type type in exportedTypes)
                    {
                        if (type.GetInterface("AOSharp.Core.IAOPluginEntry") == null)
                            continue;

                        MethodInfo method = type.GetMethod("Run", BindingFlags.Public | BindingFlags.Instance);

                        if (method == null) //Notify of plugin error somewhere?
                            continue;

                        ConstructorInfo contructor = type.GetConstructor(Type.EmptyTypes);

                        if (contructor == null)
                            continue;

                        object instance = contructor.Invoke(null);

                        if (instance == null) //Is this even possible?
                            continue;

                        method.Invoke(instance, null);
                    }
                }
                catch (Exception ex)
                {
                    //return null;
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }
    }
}
