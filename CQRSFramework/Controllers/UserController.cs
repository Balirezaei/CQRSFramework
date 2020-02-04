using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSFramework.Facade.Query;
using Framework.ApplicationService.Contract;
using Framework.ApplicationService.Contract.User;
using Framework.Core;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserQueryFacade _userQueryFacade;

        //private readonly IBaseCommandHandler<CreateUserCommand> handler;

        //public UserController(IBaseCommandHandler<CreateUserCommand> handler)
        //{
        //    this.handler = handler;
        //}


        public UserController(ICommandBus commandBus, IUserQueryFacade userQueryFacade)
        {
            _commandBus = commandBus;
            _userQueryFacade = userQueryFacade;
        }
        [HttpGet]
        public List<UserDto> GetAllUser()
        {
            return _userQueryFacade.GetAll(new PagingContract());
        }

        // POST: api/User
        [HttpPost]
        [Authorize]
        public async Task Post([FromBody] CreateUserCommand userCommand)
        {
            var userClaims = HttpContext.User.Claims;
            _commandBus.Dispatch<CreateUserCommand>(userCommand);
        }

        [HttpPut]
        public void Deactive(DeactiveUserCommand userCommand)
        {
            _commandBus.Dispatch<DeactiveUserCommand>(userCommand);
        }

    }

}
