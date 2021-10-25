using AutoMapper;
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Mapper
{
    public class MappingDtos : Profile
    {
        public MappingDtos()
        {
            CreateMap<EmployeeId, IdDto>().ReverseMap();
            CreateMap<Domain.Entities.Employee, EmployeeDto>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id.Value)).ReverseMap();

            CreateMap<EmployeeDto, Domain.Entities.Employee>()
                .ForMember(s => s.Id, o => o.MapFrom(k => new EmployeeId(k.Id.Value))).ReverseMap();

            CreateMap<NameDto, Name>().ReverseMap();
            CreateMap<RegistrationNumberDto, RegistrationNumber>().ReverseMap();
            CreateMap<Pesel, PeselDto>().ForMember(p=>p.Value, opt=>opt.MapFrom(src=>src.Value)).ReverseMap();

        }
    }
}
