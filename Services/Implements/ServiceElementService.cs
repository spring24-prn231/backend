using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class ServiceElementService : BaseService<ServiceElement>, IServiceElementService
    {
        public ServiceElementService(IBaseRepository<ServiceElement> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
