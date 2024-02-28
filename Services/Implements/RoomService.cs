using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class RoomService : BaseService<Room>, IRoomService
    {
        public RoomService(IBaseRepository<Room> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
