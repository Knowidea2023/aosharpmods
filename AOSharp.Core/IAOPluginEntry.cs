using Serilog;
using Serilog.Core;
using Logger = Serilog.Core.Logger;
using System;
using AOSharp.Core.Logging;
using AOSharp.Core.IPC;
using AOSharp.Core.UI;
using SmokeLounge.AOtomation.Messaging.Exceptions;
using System.Reflection;

namespace AOSharp.Core
{
    public interface IAOPluginEntry
    {
        void Init(string pluginDir);
        void Teardown();
    }

    public abstract class AOPluginEntry : IAOPluginEntry
    {
        protected string PluginDirectory { get; private set; }
        protected Logger Logger;

        public void Init(string pluginDir)
        {
            PluginDirectory = pluginDir;
            SetupLogging();
            LoadIPCMessages();
            Run();
        }

        private void SetupLogging()
        {
            var loggerConfig = new LoggerConfiguration();
            OnConfiguringLogger(loggerConfig);
            Logger = loggerConfig.CreateLogger();
        }

        public virtual void Run()
        {
            Run(PluginDirectory);
        }

        [Obsolete]
        public virtual void Run(string pluginDir)
        {

        }

        public virtual void Teardown() 
        {
        }

        protected virtual void OnConfiguringLogger(LoggerConfiguration loggerConfig)
        {
            if (Game.IsAOLite)
                loggerConfig.WriteTo.Console();
            else
                loggerConfig.WriteTo.Chat();

            loggerConfig.MinimumLevel.Debug();
        }

        private void LoadIPCMessages()
        {
            try
            {
                IPCChannel.LoadMessages(Assembly.GetExecutingAssembly());
            }
            catch (ContractIdCollisionException e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}
