using AutoFilterer.Extensions;
using AutoMapper;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class NotificationService : BaseService<Notification>, INotificationService
    {
        public NotificationService(IBaseRepository<Notification> repository, IMapper mapper): base(repository,mapper)
        {
            
        }

        public override IQueryable<Notification> Get<TFilter>(TFilter filter)
        {
            return _repo.GetAll().ApplyFilter(filter).AsNoTracking();
        }

        public override async Task<Notification> Create<TReq>(TReq entity)
        {
            if (entity is Notification newEntity)
            {
                newEntity.Status = false;
                return await base.Create(newEntity);
            }
            return await base.Create(entity);
        }

        public async Task Confirm(Guid id)
        {
            var entity = await _repo.GetById(id);
            if (entity != null)
            {
                entity.Status = true;
                await _repo.Update(entity);
            }
            else
            {
                throw new NotFoundException();
            }
        }
    }
}
