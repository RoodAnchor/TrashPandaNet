using Microsoft.Extensions.Logging;
using TrashPandaNet.Logic.Services.Logging.Providers;

namespace TrashPandaNet.Logic.Services.Logging.Extensions
{
    public static class FileLoggerExtension
    {
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder)
        {
            builder.AddProvider(new FileLoggerProvider());

            return builder;
        }
    }
}
