

using Framework.Core.Base;

namespace Framework.Core.CommandBus
{
    public interface ICommandBus
    {
        TResult Dispatch<T, TResult>(T command) where T : Command;
    }
}
