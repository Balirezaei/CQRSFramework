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
        public void Dispatch<T>(T command) where T : Command
        {
            IBaseCommandHandler<T> handler = null;
            try
            {
//                handler = new AuthorizeCommandHandlerDecorator<T>((IBaseCommandHandler<T>)services.GetService(typeof(AuthorizeCommandHandlerDecorator<T>)));
                handler = (IBaseCommandHandler<T>)services.GetService((typeof(LoggingHandlerDecorator<T>)));

                handler = new AuthorizeCommandHandlerDecorator<T>(handler);

                //                ((IBaseCommandHandler<T>) services.GetService(typeof(AuthorizeCommandHandlerDecorator<T>)));
                //                handler =(IBaseCommandHandler<T>) services.GetService((typeof(LoggingHandlerDecorator<T>)));
                //      handler =(IBaseCommandHandler<T>) services.GetService(typeof(LoggingHandlerDecorator<T>(typeof(LoggingHandlerDecorator<T>))));
                //handler = services.GetService<LoggingHandler<T>>();
                handler.Handle(command);
            }
            catch (Exception ex)
            {
                //Developer Mistake
                Debug.Assert(true, ex.Message);
            }
            finally
            {

            }
        }
    }
}
