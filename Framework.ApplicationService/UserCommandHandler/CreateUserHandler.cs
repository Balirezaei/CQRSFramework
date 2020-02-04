using Framework.ApplicationService.Contract;
using Framework.ApplicationService.Contract.User;
using Framework.Core;
using Framework.Domain;
using Framework.Persistense.EF;

namespace Framework.ApplicationService.UserCommandHandler
{
    public class CreateUserHandler : IBaseCommandHandler<CreateUserCommand>
    {
        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;

        public void Handle(CreateUserCommand cmd)
        {
            var user = new User(cmd.UserName, cmd.Email, cmd.Password);
            //Context.AddAsync(user);
            //Context.SaveChangesAsync();
        }
    }
}
