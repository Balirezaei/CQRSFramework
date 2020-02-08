using System.Diagnostics;
using Newtonsoft.Json;

namespace Framework.Core
{
    public class LoggingHandlerDecorator<T> : IBaseCommandHandler<T> where T : Command
    {
        private readonly ILogManagement _log;

        public LoggingHandlerDecorator(IBaseCommandHandler<T> next, ILogManagement log)
        {
            _log = log;
            _next = next;
        }

        public IBaseCommandHandler<T> _next { get; }

        public void Handle(T cmd)
        {
            _log.DoLog(cmd);
//            Debug.WriteLine(JsonConvert.SerializeObject(cmd));
            _next.Handle(cmd);
        }

    }
}
