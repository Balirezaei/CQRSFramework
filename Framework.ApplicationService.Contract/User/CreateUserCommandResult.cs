namespace Framework.ApplicationService.Contract.User
{
    public class CreateUserCommandResult
    {
        public CreateUserCommandResult(int userId)
        {
            UserId = userId;
        }

        public int UserId { get;private set; }
    }
}