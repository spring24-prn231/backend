using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<UpdateStaffRequest, ApplicationUser>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
