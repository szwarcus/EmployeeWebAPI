using EmployeeWebAPI.Application.CQRS.Dto;
using EmployeeWebAPI.Domain.Enums;
using System;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee
{
    public class EmployeeViewModel
    {
        public Guid EmployeeId { get; set; }
        public NameDto Name { get; set; }
        public PeselDto Pesel { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public RegistrationNumberDto RegistrationNumber { get; set; }
    }
}
