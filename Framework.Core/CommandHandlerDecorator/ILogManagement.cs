namespace Framework.Core.CommandHandlerDecorator
{
    public interface ILogManagement
    {
        void DoLog<T>(T command);
    }
}