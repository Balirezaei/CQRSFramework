using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Framework.Core.LogCommandHandler
{
    public class LoggingHandler<T> : IBaseCommandHandler<T> where T : Command
    {

        public LoggingHandler(IBaseCommandHandler<T> next)
        {
            _next = next;
        }

        public IBaseCommandHandler<T> _next { get; }

        public void Handle(T cmd)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(cmd));
            _next.Handle(cmd);
        }

    }
}
