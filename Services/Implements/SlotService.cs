using AutoMapper;
using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implements
{
    public class SlotService : BaseService<Slot>, ISlotService
    {
        public SlotService(IBaseRepository<Slot> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
