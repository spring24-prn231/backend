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
        public virtual void Create(T entity)
        {
            for (int i=0;i<=2;i++)
            {
                try
                {
                    _dbSet.Add(entity);
                    _context.SaveChanges();
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
        public virtual void Update(T entity)
        {
            for (int i = 0; i <= 2; i++)
            {
                try
                {
                    var tracker = _context.Attach(entity);
                    tracker.State = EntityState.Modified;
                    _context.SaveChanges();
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
        public virtual void Delete(T entity)
        {
            for (int i = 0; i <= 2; i++)
            {
                try
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
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

        public T? GetById(Guid id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public T? GetByIdNoTracking(Guid id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(x=>x.Id == id);
        }
    }
}
