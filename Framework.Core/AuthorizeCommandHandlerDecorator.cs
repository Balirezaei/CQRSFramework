namespace Framework.Core
{
    public class AuthorizeCommandHandlerDecorator<T> : IBaseCommandHandler<T> where T : Command
    {
      

        public AuthorizeCommandHandlerDecorator(IBaseCommandHandler<T> next)
        {
          
            _next = next;
        }

        public IBaseCommandHandler<T> _next { get; }

        public void Handle(T cmd)
        {
            
            //            Debug.WriteLine(JsonConvert.SerializeObject(cmd));
            _next.Handle(cmd);
        }

    }
}