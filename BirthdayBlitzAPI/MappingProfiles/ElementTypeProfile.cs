using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class ElementTypeProfile : Profile
    {
        public ElementTypeProfile()
        {
            CreateMap<CreateElementTypeRequest, ElementType>();
            CreateMap<UpdateElementTypeRequest, ElementType>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}