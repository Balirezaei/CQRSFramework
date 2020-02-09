namespace Framework.Core
{
    public class AuthorizeCommandHandlerDecorator<T, TResult> : IBaseCommandHandler<T,TResult> where T : Command
    {
      

        public AuthorizeCommandHandlerDecorator(IBaseCommandHandler<T, TResult> next)
        {
          
            _next = next;
        }

        public IBaseCommandHandler<T, TResult> _next { get; }

        public TResult Handle(T cmd)
        {
            
            //            Debug.WriteLine(JsonConvert.SerializeObject(cmd));
           return _next.Handle(cmd);
        }

    }
}