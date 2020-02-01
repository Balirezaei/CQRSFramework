using Framework.ApplicationService.Contract;
using Framework.Core;
using Framework.Persistense.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.ApplicationService.UserCommandHandler
{
 

    public class DeactiveUserHandler : IBaseCommandHandler<DeactiveUserCommand>
    {
        public DeactiveUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;

        public void Handle(DeactiveUserCommand cmd)
        {
            //var user = Context.Users.Where(m => m.Id == cmd.Id).FirstOrDefault();
            //user.IsActive = false;
            //Context.SaveChanges();
        }
    }
}
