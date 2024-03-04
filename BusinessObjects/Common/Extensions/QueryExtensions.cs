using AutoFilterer.Extensions;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Responses;
using X.PagedList;

namespace BusinessObjects.Common.Extensions
{
    public static class QueryExtensions
    {
        public static AppResponse<IEnumerable<T>>? GetPaginatedResponse<T>(this IQueryable<T> source, int page = 1, int pageSize = 10)
        {
            var paginatedResult = source.ToPagedList(page, pageSize);
            if (paginatedResult.TotalItemCount == 0) throw new NotFoundException();
            return new PaginationResponse<IEnumerable<T>>
            {
                PageSize = pageSize,
                TotalPage = paginatedResult.PageCount,
                CurrentPage = page,
                Data = paginatedResult
            };
        }
        public static IQueryable<T> GetQueryStatusTrue<T>(this IQueryable<T> source) where T : BaseModel
        {
            return source.Where(x => x.Status == true);
        }
    }
}
