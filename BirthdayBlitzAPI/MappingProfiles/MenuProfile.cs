using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
           CreateMap<CreateMenuRequest, Menu>();
           CreateMap<UpdateMenuRequest, Menu>()
               .ForMember(x => x.Id, opt => opt.Ignore())
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
        }
    }
}
