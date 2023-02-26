using AutoMapper;
using EmployeeWebAPI.Application.CQRS.Dto;
using EmployeeWebAPI.Domain.ValueObjects;

namespace EmployeeWebAPI.Application.CQRS.Mapper
{
    public class MappingDtos : Profile
    {
        public MappingDtos()
        {
            CreateMap<Domain.Entities.Employee, EmployeeDto>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id)).ReverseMap();

            CreateMap<EmployeeDto, Domain.Entities.Employee>()
                .ForMember(s => s.Id, o => o.MapFrom(k => k.Id)).ReverseMap();

            CreateMap<NameDto, Name>().ReverseMap();
            CreateMap<RegistrationNumberDto, RegistrationNumber>().ReverseMap();
            CreateMap<Pesel, PeselDto>().ForMember(p=>p.Value, opt=>opt.MapFrom(src=>src.Value)).ReverseMap();

        }
    }
}
