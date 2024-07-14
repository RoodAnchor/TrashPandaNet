using Microsoft.Extensions.Logging;
using System.Reflection;
using TrashPandaNet.Logic.Utils;

namespace TrashPandaNet.Logic.Services.Logging.Loggers
{
    public class FileLogger : ILogger, IDisposable
    {
        private readonly string _eventsLogPath;
        private readonly string _errorsLogPath;
        private readonly object _lock = new object();

        public FileLogger()
        {
            _eventsLogPath = GetLogsPath("Events");
            _errorsLogPath = GetLogsPath("Errors");
        }

        public IDisposable? BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose() { }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Error:
                case LogLevel.Critical:
                    WriteEntry(_errorsLogPath, formatter(state, exception));
                    break;

                default:
                    WriteEntry(_eventsLogPath, formatter(state, null));
                    break;
            }
        }

        private void WriteEntry(string path, string message)
        {
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var logPath = Path.Combine(path, $"{date}.txt");
            var file = FileUtils.CreateFile(logPath);

            lock (_lock)
            {
                using (var sw = file.AppendText())
                {
                    sw.WriteLine($"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] {message}");
                };
            }
        }

        private string GetLogsPath(string typeName)
        {
            var currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var logsPath = Path.Combine(currentLocation, "Logs");
            var typeLogsPath = Path.Combine(logsPath, typeName);

            FileUtils.CreateFolder(logsPath);
            FileUtils.CreateFolder(typeLogsPath);

            return typeLogsPath;
        }
    }
}
