using AutoMapper;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class ServiceService : BaseService<Service>, IServiceService
    {
        private readonly IMenuService _menuService;
        private readonly IServiceElementDetailService _serviceElementDetailService;
        public ServiceService(IBaseRepository<Service> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public void Update(UpdateServiceRequest request)
        {
            var service = _repo.GetAll().Where(x => x.Id == request.Id).Include(x => x.Menus == request.DishIds).FirstOrDefault();
            if (service != null)
            {
                _mapper.Map(request, service);
                _repo.Update(service);
                foreach (var elementId in request.ServiceElementIds)
                {
                        var serviceElementDetail = new ServiceElementDetail
                        {
                            ServiceId = service.Id,
                            ServiceElementId = elementId
                        };
                        _serviceElementDetailService.Create(serviceElementDetail);
                }

                foreach (var dishId in request.DishIds)
                {
                        var menu = new Menu
                        {
                            ServiceId = service.Id,
                            DishId = dishId
                        };
                        _menuService.Create(menu);
                }
            }
            else
            {
                throw new NotFoundException();
            }
        }
    }
}
