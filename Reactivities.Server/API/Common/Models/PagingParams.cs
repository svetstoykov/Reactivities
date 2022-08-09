namespace API.Common.Models
{
    public class PagingParams : BaseApiModel
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
