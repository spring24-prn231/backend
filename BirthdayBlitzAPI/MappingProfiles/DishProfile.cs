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
        }
    }
}
