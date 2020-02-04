using System.Collections.Generic;
using Framework.ApplicationService.Contract;
using Framework.Core;

namespace Framework.ApplicationService.UserQueryHandler
{
    public class GetUserQueryHandler: IBaseQueryHandler<PagingContract,List<UserDto>>
    {
        public List<UserDto> Handle(PagingContract query)
        {
            throw new System.NotImplementedException();
        }
    }
}