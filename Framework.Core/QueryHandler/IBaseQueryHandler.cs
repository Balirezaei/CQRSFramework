namespace Framework.Core.QueryHandler
{
    public interface IBaseQueryHandler<TQuery,TResult> //where TQuery : Query
    {
        TResult Handle(TQuery query);
    }
}