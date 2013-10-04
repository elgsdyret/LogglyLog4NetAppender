using System;
using Xunit;

namespace LogglyLog4NetAppenderTests
{
    public class CanLogSomething
    {		
		[Fact]
		public void Log4NetConfiguredWithLoggly_LogsSomething()
		{
			// key missing
			TestLogger.Build("some key");
			var log = log4net.LogManager.GetLogger("testing");

			var json = @"{ ""a"" : ""b"", ""c"" : " + 1 + "}";
			log.Error(json);

			// could do something clever with event - but currently just terminate manually when entry is seen in loggly
			while(true) {}
		}


    }
}
