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
        IQueryable<T> GetAll(bool eager = true);
        IQueryable<T> Get<TFilter>(TFilter filter) where TFilter : BasePaginationRequest;
        Task<T?> GetById(Guid id);
        Task<T?> GetByIdNoTracking(Guid id);
        Task<T> Create<TReq>(TReq entity) where TReq : class;
        Task<T> Update<TReq>(TReq entityRequest) where TReq: BaseUpdateRequest;
        Task Delete(Guid id);
    }
}
