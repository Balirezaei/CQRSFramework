using System;
using System.Diagnostics;


namespace Framework.Core
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
            //try
            //{
            //                handler = new AuthorizeCommandHandlerDecorator<T>((IBaseCommandHandler<T>)services.GetService(typeof(AuthorizeCommandHandlerDecorator<T>)));
            handler = (IBaseCommandHandler<T, TResult>)services.GetService((typeof(LoggingHandlerDecorator<T, TResult>)));

            handler = new AuthorizeCommandHandlerDecorator<T, TResult>(handler);

            //                ((IBaseCommandHandler<T>) services.GetService(typeof(AuthorizeCommandHandlerDecorator<T>)));
            //                handler =(IBaseCommandHandler<T>) services.GetService((typeof(LoggingHandlerDecorator<T>)));
            //      handler =(IBaseCommandHandler<T>) services.GetService(typeof(LoggingHandlerDecorator<T>(typeof(LoggingHandlerDecorator<T>))));
            //handler = services.GetService<LoggingHandler<T>>();
            return handler.Handle(command);
            //}
            //catch (Exception ex)
            //{
            //    //Developer Mistake
            //    Debug.Assert(true, ex.Message);
            //}
            //finally
            //{

        }

    }

}
