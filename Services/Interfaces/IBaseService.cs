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
        T? GetById(Guid id);
        T? GetByIdNoTracking(Guid id);
        void Create<TReq>(TReq entity) where TReq : class;
        void Update<TReq>(TReq entityRequest) where TReq: BaseUpdateRequest;
        void Delete(Guid id);
    }
}
