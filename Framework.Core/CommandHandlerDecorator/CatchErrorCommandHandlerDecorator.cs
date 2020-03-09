using System;
using Framework.Core.Base;

namespace Framework.Core.CommandHandlerDecorator
{
    public class CatchErrorCommandHandlerDecorator<T, TResult> : IBaseCommandHandler<T, TResult> where T : Command
    {


        public CatchErrorCommandHandlerDecorator(IBaseCommandHandler<T, TResult> next)
        {

            _next = next;
        }

        public IBaseCommandHandler<T, TResult> _next { get; }

        public TResult Handle(T cmd)
        {
            try
            {
                return _next.Handle(cmd);
            }
            catch (Exception e)
            {
                //Log the Eddor 
                Console.WriteLine(e);
                throw e;
            }
        }

    }
}