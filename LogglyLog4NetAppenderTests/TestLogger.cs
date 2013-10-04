
using log4net.Config;
using log4net.Layout;
using lgly = LogglyLog4NetAppender;

namespace LogglyLog4NetAppenderTests
{
	public class TestLogger
	{		
		public static void Build(string logglyKey)
		{						
			var appender = new lgly.LogglyLog4NetAppender(logglyKey) {Layout = new PatternLayout("%message")};
			appender.ActivateOptions();

			BasicConfigurator.Configure(appender);
		}
	}
}