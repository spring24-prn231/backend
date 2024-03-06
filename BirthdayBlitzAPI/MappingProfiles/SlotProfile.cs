using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class SlotProfile : Profile
    {
        public SlotProfile()
        {
            CreateMap<CreateSlotRequest, Slot>();
            CreateMap<UpdateSlotRequest, Slot>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Room, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}