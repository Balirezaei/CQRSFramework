

namespace Framework.Core
{
    public interface IBaseCommandHandler<T,TResult> where T : Command
    {
        TResult Handle(T cmd);
    }
}
