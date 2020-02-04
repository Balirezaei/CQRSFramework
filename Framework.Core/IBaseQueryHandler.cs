namespace Framework.Core
{
    public interface IBaseQueryHandler<TQuery,TResult> //where TQuery : Query
    {
        TResult Handle(TQuery query);
    }
}