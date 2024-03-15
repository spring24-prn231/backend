using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class PartyPlanProfile : Profile
    {
        public PartyPlanProfile() 
        {
            CreateMap<CreatePartyPlanRequest, PartyPlan>();
            CreateMap<UpdatePartyPlanRequest, PartyPlan>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
