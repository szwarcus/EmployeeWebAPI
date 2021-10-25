using EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees;
using EmployeeWebAPI.Domain.ValueObjects.Ids;

namespace EmployeeWebAPI.Application.CQRS.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeCommand, Domain.Entities.Employee>()
                .ForMember(dest => dest.Pesel, opts => opts.MapFrom(y => y.Pesel))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(y => y.Name));

            CreateMap<UpdateEmployeeCommand, Domain.Entities.Employee>()
               .ForMember(dest => dest.Pesel, opts => opts.MapFrom(y => y.Pesel))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(y => y.Name));
            CreateMap<EmployeeDto, Domain.Entities.Employee>().ReverseMap();

            CreateMap<Domain.Entities.Employee, EmployeeViewModel>()
                .ForMember(dest=>dest.EmployeeId, opts=>opts.MapFrom(y=>y.Id))
                .ReverseMap();
            CreateMap<EmployeesInListViewModel, Domain.Entities.Employee>()
               .ForMember(s => s.Id, o => o.MapFrom(e => new EmployeeId(e.EmployeeId.Value)));
            CreateMap<Domain.Entities.Employee, EmployeesInListViewModel>()
                 .ForMember(s => s.EmployeeId, o => o.MapFrom(k => k.Id));

        }
    }
}
