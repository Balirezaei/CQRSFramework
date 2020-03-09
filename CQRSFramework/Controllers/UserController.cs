using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSFramework.Facade.Query;
using Framework.ApplicationService.Contract;
using Framework.ApplicationService.Contract.User;
using Framework.Core;
using Framework.Core.CommandBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        //[EnableCors("AllowOrigin")]
        [Authorize]
        public List<UserDto> GetAllUser()
        {
            return _userQueryFacade.GetAll(new PagingContract());
        }

        // POST: api/User
        [HttpPost]
      //  [Authorize]
        public int Post([FromBody] CreateUserCommand userCommand)
        {
            var userClaims = HttpContext.User.Claims;
            return _commandBus.Dispatch<CreateUserCommand, CreateUserCommandResult>(userCommand).UserId;
        }

        [HttpPut]
        public void Deactive(DeactiveUserCommand userCommand)
        {
            _commandBus.Dispatch<DeactiveUserCommand, Nothing>(userCommand);
        }

    }

}
