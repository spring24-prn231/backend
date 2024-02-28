using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class OrderDetailService : BaseService<OrderDetail>, IOrderDetailService
    {
        public OrderDetailService(IBaseRepository<OrderDetail> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
