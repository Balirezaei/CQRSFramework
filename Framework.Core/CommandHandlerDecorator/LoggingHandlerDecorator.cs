using Framework.Core.Base;

namespace Framework.Core.CommandHandlerDecorator
{
    public class LoggingHandlerDecorator<T, TResult> : IBaseCommandHandler<T, TResult> where T : Command
    {
        private readonly ILogManagement _log;

        public LoggingHandlerDecorator(IBaseCommandHandler<T, TResult> next, ILogManagement log)
        {
            _log = log;
            _next = next;
        }

        public IBaseCommandHandler<T, TResult> _next { get; }

        public TResult Handle(T cmd)
        {
            _log.DoLog(cmd);
            //            Debug.WriteLine(JsonConvert.SerializeObject(cmd));
            return _next.Handle(cmd);
        }

    }
}
