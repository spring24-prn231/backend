using AutoMapper;
using BusinessObjects.Common.Exceptions;
using BusinessObjects.Models;
using BusinessObjects.Requests;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class OrderDetailService : BaseService<OrderDetail>, IOrderDetailService
    {
        private readonly IOrderService _orderService;
        public OrderDetailService(IBaseRepository<OrderDetail> repository, IMapper mapper, IOrderService orderService) : base(repository, mapper)
        {
            _orderService = orderService;
        }

        public override async Task<OrderDetail> Create<TReq>(TReq entity)
        {
            if (entity is CreateOrderDetailRequest newEntityReq)
            {
                var newEntity = _mapper.Map<OrderDetail>(newEntityReq);

                var response = await _repo.Create(newEntity);
                var order = await _orderService.GetByIdNoTracking(newEntity.OrderId.Value);
                var orderUpdate = new UpdateOrderRequest
                {
                    Total = (newEntity.Price*newEntity.Amount) + order.Total,
                    Id = newEntity.OrderId.Value,
                };
                await _orderService.Update(orderUpdate);
                return response;
            }
            else
            {
                throw new ValidationException();
            }

        }

        public override async Task<OrderDetail> Update<TReq>(TReq entityRequest)
        {
            if (entityRequest is UpdateOrderDetailRequest orderDetailUpdate)
            {
                var entity = await _repo.GetById(entityRequest.Id);
                if (entity != null)
                {
                    var order = await _orderService.GetByIdNoTracking(entity.OrderId.Value);
                    var orderUpdate = new UpdateOrderRequest
                    {
                        Total = order.Total - (entity.Price * entity.Amount),
                        Id = entity.OrderId.Value,
                    };
                    _mapper.Map(entityRequest, entity);
                    order.Total += entity.Price * entity.Amount;
                    var response = await _repo.Update(entity);
                    await _orderService.Update(orderUpdate);
                    return response;
                }
                else
                {
                    throw new NotFoundException();
                }
            }
            else
            {
                throw new ValidationException();
            }
        }
    }
}
