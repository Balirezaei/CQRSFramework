using Framework.Core;

namespace Framework.ApplicationService.Contract
{
    public class PagingContract:Query
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}