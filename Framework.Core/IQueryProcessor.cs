namespace Framework.Core
{
    public interface IQueryProcessor
    {
        TResult Process<TQuery, TResult>(TQuery command);
    }
}