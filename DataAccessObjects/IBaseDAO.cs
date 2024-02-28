using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public interface IBaseDAO<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        T? GetById(Guid id);
        T? GetByIdNoTracking(Guid id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }   
}
