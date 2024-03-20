using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly IBaseDAO<T> _dao;
        public BaseRepository(IBaseDAO<T> dao)
        {
            _dao = dao;
        }
        public virtual async Task<T> Create(T entity)
        {
            return await _dao.Create(entity);
        }

        public virtual async Task Delete(T entity)
        {
            entity.Status = false;
            await _dao.Update(entity);
        }

        public virtual IQueryable<T> GetAll(bool eager = true)
        {
            return _dao.GetAll(eager);
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            return await _dao.GetById(id);
        }

        public virtual async Task<T?> GetByIdNoTracking(Guid id)
        {
            return await _dao.GetByIdNoTracking(id);
        }

        public async Task HardDelete(T entity)
        {
            await _dao.Delete(entity);
        }

        public virtual async Task<T> Update(T entity)
        {
            return await _dao.Update(entity);
        }
    }
}
