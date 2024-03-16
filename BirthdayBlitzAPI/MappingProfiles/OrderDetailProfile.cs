using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile() 
        {
            CreateMap<CreateOrderDetailRequest, OrderDetail>();
            CreateMap<UpdateOrderDetailRequest, OrderDetail>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
