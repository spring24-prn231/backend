using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        IQueryable<T> GetAll(bool eager = true);
        Task<T?> GetById(Guid id);
        Task<T?> GetByIdNoTracking(Guid id);
    }
}
