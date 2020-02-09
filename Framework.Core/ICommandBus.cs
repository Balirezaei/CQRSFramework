

namespace Framework.Core
{
    public interface ICommandBus
    {
        TResult Dispatch<T, TResult>(T command) where T : Command;
    }
}
