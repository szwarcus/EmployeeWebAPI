using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Domain.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        public EmployeeFactory()
        {

        }

        public Employee CreateEmployee(string firstName,string lastName, string pesel, DateTime birthDate, Gender gender)
        {
            return new Employee(new Name(firstName,lastName), birthDate, new Pesel(pesel), new RegistrationNumber(), gender, new ValueObjects.Ids.EmployeeId());
        }
    }
}
