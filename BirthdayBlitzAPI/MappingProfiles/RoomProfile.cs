using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile() 
        {
            CreateMap<CreateRoomRequest, Room>();
            CreateMap<CreateSlotDto, Slot>();
            CreateMap<UpdateSlotDto, Slot>();
            CreateMap<UpdateRoomRequest, Room>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}