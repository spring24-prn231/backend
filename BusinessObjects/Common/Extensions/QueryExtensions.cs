using AutoFilterer.Extensions;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Responses;

namespace BusinessObjects.Common.Extensions
{
    public static class QueryExtensions
    {
        public static AppResponse<IEnumerable<T>>? GetPaginatedResponse<T>(this IQueryable<T> source, int page = 1, int pageSize = 10)
        {
            var itemCount = source.Count();
            if (itemCount == 0) throw new NotFoundException();
            return new PaginationResponse<IEnumerable<T>>
            {
                PageSize = pageSize,
                TotalPage = (int)Math.Ceiling((decimal)itemCount / pageSize),
                CurrentPage = page,
                Data = source.ToPaged(page, pageSize).ToList()
            };
        }
        public static IQueryable<T> GetQueryStatusTrue<T>(this IQueryable<T> source) where T : BaseModel
        {
            return source.Where(x => x.Status == true);
        }
    }
}
