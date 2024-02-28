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
        public virtual void Create(T entity)
        {
            _dao.Create(entity);
        }

        public virtual void Delete(T entity)
        {
            entity.Status = false;
            _dao.Update(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dao.GetAll();
        }

        public virtual T? GetById(Guid id)
        {
            return _dao.GetById(id);
        }

        public virtual T? GetByIdNoTracking(Guid id)
        {
            return _dao.GetByIdNoTracking(id);
        }

        public virtual void Update(T entity)
        {
            _dao.Update(entity);
        }
    }
}
