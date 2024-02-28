using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class ServiceService : BaseService<Service>, IServiceService
    {
        public ServiceService(IBaseRepository<Service> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
