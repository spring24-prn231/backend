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

        public override async Task<Service> Update<TReq>(TReq entityRequest)
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
                        foreach (var detail in serviceElementDetails)
                        {
                            await _serviceElementDetailService.Delete(detail.Id);
                        }
                    }

                    var menus = await _menuService.GetAll().Where(x => x.ServiceId == service.Id).ToListAsync();
                    if (menus != null)
                    {
                        foreach (var menu in menus)
                        {
                            await _menuService.Delete(menu.Id);
                        }
                    }

                    //create new serviceelementdetails and menus
                    foreach (var elementId in request.ServiceElementIds)
                    {
                        var serviceElementDetail = new ServiceElementDetail
                        {
                            ServiceId = service.Id,
                            ServiceElementId = elementId
                        };
                        await _serviceElementDetailService.Create(serviceElementDetail);
                    }
                    foreach (var dishId in request.DishIds)
                    {
                        var menu = new Menu
                        {
                            ServiceId = service.Id,
                            DishId = dishId
                        };
                        await _menuService.Create(menu);
                    }
                    return await _repo.Update(service);
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

        public override async Task<Service> Create<TReq>(TReq entityRequest)
        {
            var request = entityRequest as CreateServiceRequest;
            var service = _mapper.Map<Service>(request);
            var response = await _repo.Create(service);
            foreach (var elementId in request.ServiceElementIds)
            {
                await _serviceElementDetailService.Create(new ServiceElementDetail
                {
                    ServiceId = service.Id,
                    ServiceElementId = elementId
                });
            }
            foreach (var dishId in request.DishIds)
            {
                await _menuService.Create(new Menu
                {
                    ServiceId = service.Id,
                    DishId = dishId
                });
            }
            return response;
        }
    }
}
