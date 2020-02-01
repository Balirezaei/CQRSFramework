

namespace Framework.Core
{
    public interface ICommandBus
    {
        void Dispatch<T>(T command) where T : Command;
    }
}
