using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implements
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService(IBaseRepository<Order> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
