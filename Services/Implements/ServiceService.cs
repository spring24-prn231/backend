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

        public ServiceService(IBaseRepository<Service> repository, IMapper mapper, IMenuService menuService, IServiceElementDetailService serviceElementDetailService) : base(repository, mapper)
        {
            _menuService = menuService;
            _serviceElementDetailService = serviceElementDetailService;
        }

        public override async Task Update<TReq>(TReq entityRequest)
        {
            var request = entityRequest as UpdateServiceRequest;
            var service = await _repo.GetAll().Where(x => x.Id == request.Id).Include(x => x.Menus).Include(x => x.ServiceElementDetails).FirstOrDefaultAsync();
            if (service != null)
            {
                _mapper.Map(request, service);
                if (request.ServiceElementIds != null && request.DishIds != null)
                {
                    //delete all serviceelementdetails and menus
                    var serviceElementDetails = await _serviceElementDetailService.GetAll().Where(x => x.ServiceId == service.Id).ToListAsync();
                    if (serviceElementDetails != null)
                    {
                        var deleteTasks = serviceElementDetails.Select(serviceElementDetail => _serviceElementDetailService.Delete(serviceElementDetail.Id));
                        await Task.WhenAll(deleteTasks);
                    }

                    var menus = await _menuService.GetAll().Where(x => x.ServiceId == service.Id).ToListAsync();
                    if (menus != null)
                    {
                        var deleteTasks = menus.Select(menu => _menuService.Delete(menu.Id));
                        await Task.WhenAll(deleteTasks);
                    }

                    //create new serviceelementdetails and menus
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
                    await _repo.Update(service);
                }
                else
                {
                    throw new NotFoundException();
                }
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public override async Task Create<TReq>(TReq entityRequest)
        {
            var request = entityRequest as CreateServiceRequest;
            var service = _mapper.Map<Service>(request);
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
            await _repo.Create(service);
        }
    }
}
