using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educationtesttask.Application.Logging
{
	public interface ISerilogLogger
	{
		void LogInformation(string message);
		void LogWarning(string message);
		void LogError(Exception exception);
		void LogCritical(Exception exception);
	}
}
