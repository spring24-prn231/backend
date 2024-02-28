using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class ServiceElementDetailService : BaseService<ServiceElementDetail>, IServiceElementDetailService
    {
        public ServiceElementDetailService(IBaseRepository<ServiceElementDetail> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
