using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcern.Logging.Abstracts
{
    public interface ILogger
    {
        public void Trace(string message);
        public void Critical(string message);
        public void Information(string message);
        public void Warning(string message);
        public void Debug(string message);
        public void Error(string message);
    }
}
