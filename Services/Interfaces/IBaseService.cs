using AutoFilterer.Abstractions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBaseService<T> where T : BaseModel
    {

        IQueryable<T> Get<TFilter>(TFilter filter) where TFilter : IFilter;
        Task<T?> GetById(Guid id);
        Task<T?> GetByIdNoTracking(Guid id);
        Task Create<TReq>(TReq entity) where TReq : class;
        Task Update<TReq>(TReq entityRequest) where TReq: BaseUpdateRequest;
        Task Delete(Guid id);
    }
}
