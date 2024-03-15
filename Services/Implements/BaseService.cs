using AutoFilterer.Abstractions;
using AutoFilterer.Extensions;
using AutoMapper;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Common.Extensions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implements
{
    public class BaseService<T> : IBaseService<T> where T : BaseModel
    {
        protected readonly IBaseRepository<T> _repo;
        protected readonly IMapper _mapper;
        public BaseService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        public virtual async Task Create<TReq>(TReq entity) where TReq : class
        {
            var newEntity = _mapper.Map<T>(entity);
            await _repo.Create(newEntity);
        }

        public virtual async Task Delete(Guid id)
        {
            var entity = await _repo.GetById(id);
            if (entity != null)
            {
                await _repo.Delete(entity);
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public virtual IQueryable<T> Get<TFilter>(TFilter filter) where TFilter : IFilter
        {
            return _repo.GetAll().GetQueryStatusTrue().ApplyFilter(filter).AsNoTracking();
        }

        public IQueryable<T> GetAll()
        {
            return _repo.GetAll();
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            var result = await _repo.GetById(id);
            return result?.Status == true ? result : null;
        }

        public virtual async Task<T?> GetByIdNoTracking(Guid id)
        {

            var result = await _repo.GetByIdNoTracking(id);
            return result?.Status == true ? result : null;
        }

        public virtual async Task Update<TReq>(TReq entityRequest) where TReq : BaseUpdateRequest
        {
            var entity = await _repo.GetById(entityRequest.Id);
            if (entity != null)
            {
                _mapper.Map(entityRequest, entity);
                await _repo.Update(entity);
            }
            else
            {
                throw new NotFoundException();
            }
        }
    }
}
