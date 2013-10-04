using System;
using System.Diagnostics;
using Loggly;
using Loggly.Responses;
using Newtonsoft.Json;
using log4net.Appender;
using log4net.Core;

namespace LogglyLog4NetAppender
{
	public class LogglyLog4NetAppender : AppenderSkeleton
	{
		private readonly Logger logglyLogger;

		public LogglyLog4NetAppender(string logglyKey)
		{
			logglyLogger = new Logger(logglyKey);			
		}

		protected override void Append(LoggingEvent loggingEvent)
		{						
			Log(RenderLoggingEvent(loggingEvent));
		}

		private void Log(string json)
		{
			// The Log() call is async
			try
			{
				logglyLogger.Log(json, true, HandleResponse);
			}
			catch (Exception ex)
			{
				LogError(JsonConvert.SerializeObject(ex));
			}
		}
		
		private static void HandleResponse(LogResponse resp)
		{
			LogError(JsonConvert.SerializeObject(resp));

			if (null == resp.Error)
			{
				return;
			}

			var description = JsonConvert.SerializeObject(resp.Error);
			LogError(description);
		}
	
		private static void LogError(string description)
		{
			var appLog = new EventLog { Source = "LogglyError" };
			appLog.WriteEntry(description, EventLogEntryType.Error, 444);
		}
	}
}