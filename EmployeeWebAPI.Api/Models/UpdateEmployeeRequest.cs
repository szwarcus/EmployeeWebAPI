using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.Enums;
using System;


namespace EmployeeWebAPI.Api.Models
{
    public class UpdateEmployeeRequest
    {
        public IdDto EmployeeId { get; set; }
        public NameDto Name { get; set; }
        public PeselDto Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public RegistrationNumberDto RegistrationNumber { get; set; }
    }
}
