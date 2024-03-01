using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishRequest, Dish>();
            CreateMap<UpdateDishRequest, Dish>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}