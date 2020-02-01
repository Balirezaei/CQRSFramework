using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.ApplicationService.Contract;
using Framework.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CQRSFramework.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        //private readonly IBaseCommandHandler<CreateUserCommand> handler;

        //public UserController(IBaseCommandHandler<CreateUserCommand> handler)
        //{
        //    this.handler = handler;
        //}


        public UserController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        // POST: api/User
        [HttpPost]
        public async Task Post([FromBody] CreateUserCommand userCommand)
        {
            _commandBus.Dispatch<CreateUserCommand>(userCommand);
        }

        [HttpPut]
        public void Deactive(DeactiveUserCommand userCommand)
        {
            _commandBus.Dispatch<DeactiveUserCommand>(userCommand);
        }

    }
}
