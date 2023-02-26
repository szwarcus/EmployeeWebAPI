using AutoMapper;
using EmployeeWebAPI.Application.CQRS.Dto;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee;
using EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees;
using EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee;

namespace EmployeeWebAPI.Application.CQRS.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeCommand, Domain.Entities.Employee>()
                .ForMember(dest => dest.Pesel, opts => opts.MapFrom(y => y.Pesel))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(y => y.Name));

            CreateMap<GetEmployeeQuery, EmployeeDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(y => y.Id));

            CreateMap<UpdateEmployeeCommand, Domain.Entities.Employee>()
               .ForMember(dest => dest.Id, opts => opts.MapFrom(y => y.EmployeeId))
               .ForMember(dest => dest.Pesel, opts => opts.MapFrom(y => y.Pesel))
               .ForMember(dest => dest.Name, opts => opts.MapFrom(y => y.Name));

            CreateMap<EmployeeDto, Domain.Entities.Employee>()
                .ReverseMap();

            CreateMap<Domain.Entities.Employee, EmployeeViewModel>()
                .ForMember(dest=>dest.EmployeeId, opts=>opts.MapFrom(y=>y.Id))
                .ReverseMap();

            CreateMap<EmployeesInListViewModel, Domain.Entities.Employee>()
               .ForMember(s => s.Id, o => o.MapFrom(e => e.Id))
               .ReverseMap();
        }
    }
}
