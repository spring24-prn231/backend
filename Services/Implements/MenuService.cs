using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        public MenuService(IBaseRepository<Menu> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
