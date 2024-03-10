using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class DishTypeProfile : Profile
    {
        public DishTypeProfile()
        {
            CreateMap<CreateDishTypeRequest, DishType>();
            CreateMap<UpdateDishTypeRequest, DishType>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
