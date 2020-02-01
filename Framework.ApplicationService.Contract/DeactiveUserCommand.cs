using Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApplicationService.Contract
{

    public class DeactiveUserCommand : Command
    {
        public int Id { get; set; }
        public DeactiveUserCommand(int id)
        {
            Id = id;
        }
        protected DeactiveUserCommand() { }
    }

}
