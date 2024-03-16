using AutoMapper;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IServiceService _serviceService;
        private readonly IMenuService _menuService;
        private readonly IServiceElementDetailService _serviceElementDetailService;
        private readonly INotificationService _notificationService;
        private readonly AzureBlobService _azureBlobService;
        public OrderService(IBaseRepository<Order> repository,
                            IMapper mapper,
                            IServiceService serviceService,
                            AzureBlobService blobService,
                            IMenuService menuService,
                            IServiceElementDetailService serviceElementDetailService,
                            INotificationService notificationService) : base(repository, mapper)
        {
            _notificationService = notificationService;
            _serviceElementDetailService = serviceElementDetailService;
            _menuService = menuService;
            _serviceService = serviceService;
            _azureBlobService = blobService;
        }

        public async Task Approve(ApprovePlanRequest request)
        {
            var order = await _repo.GetAll().FirstOrDefaultAsync(x=>x.Id == request.OrderId);
            order.ExecutionStatus = (int)OrderStatus.EXECUTING;
            await _repo.Update(order);
            var staffUserName = order.Staff.UserName;
            await _notificationService.Create(new Notification() { Content = "Order của bạn đã được duyệt!", Role = staffUserName });
        }

        public async Task AssignStaff(AssignStaffRequest request, string staffUserName)
        {
            var order = await _repo.GetById(request.OrderId);
            order.ExecutionStatus = (int)OrderStatus.ASSIGNED;
            order.StaffId = request.StaffId;
            await _repo.Update(order);
            await _notificationService.Create(new Notification() { Content = "Order mới được phân công cho bạn!", Role = staffUserName });
        }

        public async Task Create<TReq>(TReq entity, Guid userId)
        {
            var createOrderRequest = entity as CreateOrderRequest;
            Service? newService = null;
            var newOrder = _mapper.Map<Order>(createOrderRequest);
            if (createOrderRequest?.NewService != null)
            {
                newService = _mapper.Map<Service>(createOrderRequest?.NewService);
                newService.UserId = userId;
                await _serviceService.Create(newService);
                newOrder.ServiceId = newService?.Id;
                //Create Menu
                foreach (var dishId in createOrderRequest?.NewService.DishIds)
                {
                    await _menuService.Create(new Menu { DishId = dishId, ServiceId = newOrder.ServiceId });
                }
                //Create ServiceElementDetail
                foreach (var serviceElementId in createOrderRequest?.NewService.ServiceElementIds)
                {
                    await _serviceElementDetailService.Create(new ServiceElementDetail { ServiceElementId = serviceElementId, ServiceId = newOrder.ServiceId.Value });
                }

            }
            else if (createOrderRequest?.RecommendServiceId != null)
            {
                newOrder.ServiceId = createOrderRequest?.RecommendServiceId;
            }
            await _repo.Create(newOrder);
            await _notificationService.Create(new Notification() { Content = "Có đơn đặt tiệc mới", Role = UserRole.ADMIN.ToString() });
        }

        public async Task<bool> DoneOrder(DoneOrderRequest request, Guid staffId)
        {
            var rs = false;
            var order = await _repo.GetById(request.OrderId);
            if (order?.StaffId == staffId)
            {
                rs = true;
                order.ExecutionStatus = (int)OrderStatus.DONE;
                await _repo.Update(order);
            }
            return rs;
        }

        public override async Task Update<TReq>(TReq entityRequest)
        {
            if (entityRequest is UpdateOrderRequest orderUpdate)
            {
                var entity = await _repo.GetById(entityRequest.Id);
                if (entity != null)
                {
                    if (orderUpdate.ContractFile != null)
                    {
                        var fileName = $"Contract_{orderUpdate.Id}.{Path.GetExtension(orderUpdate?.ContractFile.FileName)}";
                        var contractLinks = await _azureBlobService.UploadFiles(new List<IFormFile> { orderUpdate.ContractFile }, fileName, StorageType.Contract);
                        entity.Contract = contractLinks.FirstOrDefault();
                    }
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
}
