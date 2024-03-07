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
        public IQueryable<T> GetAll();
        public IQueryable<T> Get<TFilter>(TFilter filter) where TFilter : IFilter;
        T? GetById(Guid id);
        T? GetByIdNoTracking(Guid id);
        public void Create<TReq>(TReq entity) where TReq : class;
        public void Update<TReq>(TReq entityRequest) where TReq: BaseUpdateRequest;
        public void Delete(Guid id);
    }
}
