using System;
using System.Diagnostics;
using Framework.ApplicationService.Contract;
using Framework.Core;
using Framework.Core.CommandHandlerDecorator;
using Newtonsoft.Json;

namespace CQRSFramework.LogManagement
{
    public class LogManagement : ILogManagement
    {
        private readonly CurrentUser _currentUser;

        public LogManagement(CurrentUser currentUser)
        {
            _currentUser = currentUser;
        }


        public void DoLog<T>(T command)
        {
            var t = command.GetType();
            var serialize = JsonConvert.SerializeObject(command);
            var log = $"{_currentUser.UserName} is doing {t.Name}  with this data ( {serialize} ) ";
            Debug.WriteLine(log);
        }
    }
}
