using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOSharp.Core.UI;
using AOSharp.Common.GameData;

namespace AOSharp.Core.Logging
{
    public static class ChatSinkExtensions
    {
        public static LoggerConfiguration Chat(this LoggerSinkConfiguration loggerConfiguration, IFormatProvider fmtProvider = null)
        {
            return loggerConfiguration.Sink(new ChatSink(fmtProvider));
        }
    }

    public class ChatSink : ILogEventSink
    {
        IFormatProvider _formatProvider;

        public ChatSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            string message = logEvent.RenderMessage(_formatProvider);

            if (logEvent.Level == LogEventLevel.Debug)
                Chat.WriteLine(message, ChatColor.Green);
            else if (logEvent.Level == LogEventLevel.Error)
                Chat.WriteLine(message, ChatColor.Red);
            else if (logEvent.Level == LogEventLevel.Warning)
                Chat.WriteLine(message, ChatColor.Orange);
            else if (logEvent.Level == LogEventLevel.Information)
                Chat.WriteLine(message);
        }
    }
}
