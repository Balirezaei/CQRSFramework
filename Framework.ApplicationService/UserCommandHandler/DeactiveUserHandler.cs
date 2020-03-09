using Framework.ApplicationService.Contract;
using Framework.Core;
using Framework.Persistense.EF;
using System;
using System.Collections.Generic;
using System.Text;
using Framework.ApplicationService.Contract.User;
using Framework.Core.CommandHandlerDecorator;

namespace Framework.ApplicationService.UserCommandHandler
{
    public class DeactiveUserHandler : IBaseCommandHandler<DeactiveUserCommand, Nothing>
    {
        public DeactiveUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;

        public Nothing Handle(DeactiveUserCommand cmd)
        {
            //var user = Context.Users.Where(m => m.Id == cmd.Id).FirstOrDefault();
            //user.IsActive = false;
            //Context.SaveChanges();

            return new Nothing();
        }
    }
}
