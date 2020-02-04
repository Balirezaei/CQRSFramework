using Framework.Core;

namespace Framework.ApplicationService.Contract.User
{
    public class CreateUserCommand : Command
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CreateUserCommand(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }
        protected CreateUserCommand() { }

    }

}
