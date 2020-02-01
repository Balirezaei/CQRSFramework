

namespace Framework.Core
{
    public interface IBaseCommandHandler<T> where T : Command
    {
         void Handle(T cmd);
    }
}
