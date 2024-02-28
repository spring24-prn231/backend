using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class RoomTypeService : BaseService<RoomType>, IRoomTypeService
    {
        public RoomTypeService(IBaseRepository<RoomType> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
