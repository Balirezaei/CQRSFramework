using System;
using System.Collections.Generic;
using Framework.ApplicationService.Contract;

namespace CQRSFramework.Facade.Query
{
    public interface IUserQueryFacade
    {
        List<UserDto> GetAll(PagingContract pagingContract);
    }
}
