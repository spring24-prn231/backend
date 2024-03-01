using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile() 
        {
            CreateMap<CreateSlotDto, Slot>();
            CreateMap<CreateRoomRequest, Room>()
                .ForMember(dest => dest.Slots, opt => opt.MapFrom(src => src.Slots));
            CreateMap<UpdateSlotDto, Slot>();
            CreateMap<UpdateRoomRequest, Room>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
