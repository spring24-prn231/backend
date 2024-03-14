using AutoFilterer.Extensions;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Responses;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.Common.Extensions
{
    public static class QueryExtensions
    {
        public static async Task<AppResponse<IEnumerable<T>>?> GetPaginatedResponse<T>(this IQueryable<T> source, int page = 1, int pageSize = 10)
        {
            var paginatedResult = await source.ToPagedListAsync(page, pageSize);
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
