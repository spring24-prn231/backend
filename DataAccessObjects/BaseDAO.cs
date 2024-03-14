using BusinessObjects.Models;
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
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public virtual async Task Create(T entity)
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
        }
        public virtual async Task Update(T entity)
        {
            for (int i = 0; i <= 2; i++)
            {
                try
                {
                    var tracker = _context.Attach(entity);
                    tracker.State = EntityState.Modified;
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
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> GetByIdNoTracking(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
