using EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;

namespace EmployeeWebAPI.Application.CQRS.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeCommand, Domain.Entities.Employee>()
                .ForMember(dest => dest.Pesel, opts => opts.MapFrom(y => y.Pesel))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(y => y.Name));
               
            CreateMap<EmployeeDto, Domain.Entities.Employee>().ReverseMap();
        }
    }
}
