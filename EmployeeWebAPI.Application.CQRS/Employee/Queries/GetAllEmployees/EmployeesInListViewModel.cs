using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.Enums;
using System;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees
{
    public class EmployeesInListViewModel
    {
        public IdDto EmployeeId { get; set; }
        public NameDto Name { get; set; }
        public PeselDto Pesel { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public RegistrationNumberDto RegistrationNumber { get; set; }
    }
}
