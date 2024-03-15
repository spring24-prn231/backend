using AutoMapper;
using BusinessObjects.Models;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUser>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
