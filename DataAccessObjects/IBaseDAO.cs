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
        Task<T?> GetById(Guid id);
        Task<T?> GetByIdNoTracking(Guid id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }   
}
