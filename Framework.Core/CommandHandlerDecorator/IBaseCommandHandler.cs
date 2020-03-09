

using Framework.Core.Base;

namespace Framework.Core.CommandHandlerDecorator
{
    public interface IBaseCommandHandler<T,TResult> where T : Command
    {
        TResult Handle(T cmd);
    }
}
