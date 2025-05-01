using Core.CrossCuttingConcern.Logging.Abstracts;
using Microsoft.AspNetCore.Builder;
using PackageSerilog = Serilog;

namespace Core.CrossCuttingConcern.Logging.Serilog
{
    public abstract class SerilogLoggerServiceBase : ILogger
    {
        public PackageSerilog.ILogger? Logger { get; set; }
     

        protected SerilogLoggerServiceBase(PackageSerilog.ILogger logger)
        {
            Logger = logger;
        }

        public void Critical(string message)
        {
            Logger?.Fatal(message);
        }

        public void Debug(string message)
        {
            Logger?.Debug(message);
        }

        public void Error(string message)
        {
            Logger?.Error(message);
        }

        public void Information(string message)
        {
            Logger?.Information(message);
        }

        public void Trace(string message)
        {
            Logger?.Verbose(message);
        }

        public void Warning(string message)
        {
            Logger?.Warning(message);
        }
    }
}
