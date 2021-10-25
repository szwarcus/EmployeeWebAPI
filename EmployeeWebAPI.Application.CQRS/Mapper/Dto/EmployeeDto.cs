using EmployeeWebAPI.Domain.Enums;
using System;

namespace EmployeeWebAPI.Application.CQRS.Mapper.Dto
{
    public class EmployeeDto
    {
        public IdDto Id {get;set;}
        public NameDto Name { get; set; } 
        public DateTime BirthDate { get; set; }
        public PeselDto Pesel { get; set; }
        public RegistrationNumberDto RegistrationNumber { get; set; }
        public Gender Gender { get; set; }
    }
}
