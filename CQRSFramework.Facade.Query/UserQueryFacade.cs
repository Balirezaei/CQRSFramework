using System.Collections.Generic;
using Framework.ApplicationService.Contract;
using Framework.Core;

namespace CQRSFramework.Facade.Query
{
    public class UserQueryFacade : IUserQueryFacade
    {
        private readonly IQueryProcessor _queryProcessor;

        public UserQueryFacade(IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public List<UserDto> GetAll(PagingContract pagingContract)
        {
            return _queryProcessor.Process<PagingContract, List<UserDto>>(pagingContract);
        }
    }
}