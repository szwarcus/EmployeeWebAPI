using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.Enums;
using System;


namespace EmployeeWebAPI.Api.Models
{
    public class CreateEmployeeRequest
    {

        public NameDto Name { get; set; }
        public PeselDto Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

    }
}
