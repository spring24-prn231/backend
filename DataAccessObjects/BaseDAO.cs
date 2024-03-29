﻿using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Common.Extensions;

namespace DataAccessObjects
{
    public class BaseDAO<T> : IBaseDAO<T> where T : BaseModel
    {
        protected readonly BirthdayBlitzContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseDAO(BirthdayBlitzContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual IQueryable<T> GetAll(bool eager = true)
        {
            return Query(eager).AsQueryable();
        }
        public virtual IQueryable<T> Query(bool eager = true)
        {
            var query = _dbSet.AsQueryable();
            if (eager)
            {
                var navigations = _context.Model.FindEntityType(typeof(T))
                    .GetDerivedTypesInclusive()
                    .SelectMany(type => type.GetNavigations())
                    .Distinct();

                foreach (var property in navigations)
                    query = query.Include(property.Name);
            }
            return query;
        }
        public virtual async Task<T> Create(T entity)
        {
            for (int i=0;i<=2;i++)
            {
                try
                {
                    _dbSet.Add(entity);
                    await _context.SaveChangesAsync();
                    break;
                }
                catch (Exception e)
                {
                    _context.ChangeTracker.Clear();
                    if(i == 2)
                    {
                        throw;
                    }
                    continue;
                }
            }
            return entity;
        }
        public virtual async Task<T> Update(T entity)
        {
            for (int i = 0; i <= 2; i++)
            {
                try
                {
                    //var tracker = _context.Attach(entity);
                    //tracker.State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    break;
                }
                catch (Exception e)
                {
                    _context.ChangeTracker.Clear();
                    if (i == 2)
                    {
                        throw;
                    }
                    continue;
                }
            }
            return entity;
        }
        public virtual async Task Delete(T entity)
        {
            for (int i = 0; i <= 2; i++)
            {
                try
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                    break;
                }
                catch (Exception e)
                {
                    _context.ChangeTracker.Clear();
                    if (i == 2)
                    {
                        throw;
                    }
                    continue;
                }
            }
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbSet.GetQueryStatusTrue().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> GetByIdNoTracking(Guid id)
        {
            return await _dbSet.GetQueryStatusTrue().AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
