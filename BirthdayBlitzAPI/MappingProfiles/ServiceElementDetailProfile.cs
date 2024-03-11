using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.Requests;

namespace BirthdayBlitzAPI.MappingProfiles
{
    public class ServiceElementDetailProfile : Profile
    {
        public ServiceElementDetailProfile()
        {
            CreateMap<CreateServiceElementDetailRequest, ServiceElementDetail>();
        }
    }
}