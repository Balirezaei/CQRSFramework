

using Framework.Core.LogCommandHandler;
using System;
using System.ComponentModel;
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
                handler =(IBaseCommandHandler<T>) services.GetService(typeof(LoggingHandler<T>));
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
