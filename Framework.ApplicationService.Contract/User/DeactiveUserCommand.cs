using Framework.Core;

namespace Framework.ApplicationService.Contract.User
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
