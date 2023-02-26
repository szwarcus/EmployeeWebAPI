using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using System;

namespace EmployeeWebAPI.Domain.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        public EmployeeFactory() { }

        public Employee CreateEmployee(string firstName,string lastName, string pesel, DateTime birthDate, Gender gender) =>
             new Employee(new Name(firstName,lastName), birthDate, new Pesel(pesel), new RegistrationNumber(), gender, EmployeeId.NewUniqueId());
    }
}
