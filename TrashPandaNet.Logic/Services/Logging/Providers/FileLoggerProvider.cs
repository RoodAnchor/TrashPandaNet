using Microsoft.Extensions.Logging;
using TrashPandaNet.Logic.Services.Logging.Loggers;

namespace TrashPandaNet.Logic.Services.Logging.Providers
{
    [ProviderAlias("FileLogger")]
    public class FileLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger();
        }

        public void Dispose() { }
    }
}
