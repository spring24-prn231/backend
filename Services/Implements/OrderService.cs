using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService(IBaseRepository<Order> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
