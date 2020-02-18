using System.Collections.Generic;
using Framework.ApplicationService.Contract;
using Framework.Core;
using Framework.Core.QueryHandler;

namespace Framework.ApplicationService.UserQueryHandler
{
    public class GetUserQueryHandler: IBaseQueryHandler<PagingContract,List<UserDto>>
    {
        public List<UserDto> Handle(PagingContract query)
        {
           return new List<UserDto>()
           {
               new UserDto(){Id = 1,FullName = "1"},
               new UserDto(){Id = 2,FullName = "2"},
           };
        }
    }
}