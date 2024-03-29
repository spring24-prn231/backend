﻿using BusinessObjects.Models;
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
        IQueryable<T> GetAll(bool eager = true);
        Task<T?> GetById(Guid id);
        Task<T?> GetByIdNoTracking(Guid id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }   
}
