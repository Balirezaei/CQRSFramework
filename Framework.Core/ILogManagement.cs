namespace Framework.Core
{
    public interface ILogManagement
    {
        void DoLog<T>(T command);
    }
}