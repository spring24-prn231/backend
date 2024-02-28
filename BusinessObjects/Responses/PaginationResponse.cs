namespace BusinessObjects.Responses
{
    public class PaginationResponse<TData> : AppResponse<TData> where TData : class
    {
        public int TotalPage = 1;
        public int CurrentPage = 1;
        public int PageSize = 10;
    }
}
