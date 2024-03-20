﻿using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task HardDelete(T entity);
        IQueryable<T> GetAll(bool eager = true);
        Task<T?> GetById(Guid id);
        Task<T?> GetByIdNoTracking(Guid id);
    }
}
