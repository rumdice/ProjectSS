using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
    public class BaseLogger<T> : ILogger<T>
    {
        private readonly string _categoryName;

        public BaseLogger()
        {
            _categoryName = typeof(T).FullName ?? "Unknown";
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // Optional: Implement if you need support for scoped logging.
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Define the log level threshold here.
            // For example, only log messages above Information level.
            return logLevel >= LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            string message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            // Format the log message with optional exception details.
            var logEntry = $"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] [{logLevel}] [{_categoryName}] {message}";

            if (exception != null)
            {
                logEntry += $"\nException: {exception}";
            }

            // Output the log entry (e.g., to console, file, or other destination).
            Console.WriteLine(logEntry);
        }

    }
}
