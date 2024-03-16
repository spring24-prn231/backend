using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class DishTypeService : BaseService<DishType>, IDishTypeService
    {
        public DishTypeService(IBaseRepository<DishType> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
