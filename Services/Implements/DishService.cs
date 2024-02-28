using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class DishService : BaseService<Dish>, IDishService
    {
        public DishService(IBaseRepository<Dish> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
