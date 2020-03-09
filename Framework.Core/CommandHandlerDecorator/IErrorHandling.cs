using System;

namespace Framework.Core.CommandHandlerDecorator
{
    public interface IErrorHandling
    {
        void HandleException(Exception exception);
    }
}