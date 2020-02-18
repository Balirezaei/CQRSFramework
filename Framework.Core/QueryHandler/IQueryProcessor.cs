namespace Framework.Core.QueryHandler
{
    public interface IQueryProcessor
    {
        TResult Process<TQuery, TResult>(TQuery command);
    }
}