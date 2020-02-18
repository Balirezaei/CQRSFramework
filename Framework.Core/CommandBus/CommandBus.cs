using System;
using Framework.Core.Base;
using Framework.Core.CommandHandlerDecorator;

namespace Framework.Core.CommandBus
{
    public class CommandBus : ICommandBus
    {
        private readonly IServiceProvider services;

        public CommandBus(IServiceProvider services)
        {
            this.services = services;
        }
        public TResult Dispatch<T, TResult>(T command) where T : Command
        {
            IBaseCommandHandler<T, TResult> handler = null;
            handler = (IBaseCommandHandler<T, TResult>)services.GetService((typeof(AuthorizeCommandHandlerDecorator<T, TResult>)));
            //            handler = (IBaseCommandHandler<T, TResult>)services.GetService((typeof(LoggingHandlerDecorator<T, TResult>)));
            var logManagement = (ILogManagement)services.GetService(typeof(ILogManagement));
            handler = new CatchErrorCommandHandlerDecorator <T, TResult>(new LoggingHandlerDecorator<T, TResult>(handler, logManagement));
            return handler.Handle(command);
        }

    }

}
