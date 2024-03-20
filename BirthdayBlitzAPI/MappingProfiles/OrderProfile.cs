using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<ServiceOrderCreate, CreateServiceRequest>();
            CreateMap<UpdateOrderRequest, Order>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
