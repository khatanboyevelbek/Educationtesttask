using Serilog;

namespace Educationtesttask.Application.Logging
{
	public class SerilogLogger : ISerilogLogger
	{
		private readonly ILogger logger;

		public SerilogLogger() 
		{
			this.logger = new LoggerConfiguration()
				.CreateLogger();
		}

		public void LogCritical(Exception exception)
		{
			this.logger.Fatal(exception, exception.Message);
		}

		public void LogError(Exception exception)
		{
			this.logger.Error(exception, exception.Message);
		}

		public void LogInformation(string message)
		{
			this.logger.Information(message);
		}

		public void LogWarning(string message)
		{
			this.logger.Warning(message);
		}
	}
}
