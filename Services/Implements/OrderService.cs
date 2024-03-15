using AutoMapper;
using BusinessObjects.Common.Constants;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IServiceService _serviceService;
        private readonly AzureBlobService _blobService;
        public OrderService(IBaseRepository<Order> repository, IMapper mapper, IServiceService serviceService, AzureBlobService blobService) : base(repository, mapper)
        {
            _serviceService = serviceService;
            _blobService = blobService;
        }

        public async Task AssignStaff(AssignStaffRequest request)
        {
            var order = await _repo.GetById(request.OrderId);
            if(order.ExecutionStatus != (int)OrderStatus.EXECUTING)
            {
                order.ExecutionStatus = (int)OrderStatus.EXECUTING;
                await _repo.Update(order);
            }
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
            }
            else if(createOrderRequest?.RecommendServiceId != null)
            {
                newOrder.ServiceId = createOrderRequest?.RecommendServiceId;
            }
            await _repo.Create(newOrder);
            //TODO Create Menu, ServiceElementDetails
        }

        public async Task<bool> DoneOrder(DoneOrderRequest request, Guid staffId)
        {
            var rs = false;
            var order = await _repo.GetById(request.OrderId);
            if(order?.StaffId == staffId)
            {
                rs = true;
                order.ExecutionStatus = (int)OrderStatus.DONE;
                await _repo.Update(order);
            }
            return rs;
        }

        public override async Task Update<TReq>(TReq entityRequest)
        {
            var orderUpdate = entityRequest as UpdateOrderRequest;
            if (orderUpdate != null)
            {
                var entity = await _repo.GetById(entityRequest.Id);
                if (entity != null)
                {
                    var fileName = $"Contract_{orderUpdate.Id}.{Path.GetExtension(orderUpdate.ContractFile.FileName)}";
                    var contractLinks = await _blobService.UploadFiles(new List<IFormFile> { orderUpdate.ContractFile }, fileName, StorageType.Contract);
                    _mapper.Map(entityRequest, entity);
                    entity.Contract = contractLinks.FirstOrDefault();
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
