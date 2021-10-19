using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Domain.Factories
{
    public interface IEmployeeFactory
    {
        Employee CreateEmployee(string firstName, string lastName, string pesel, DateTime birthDate, Gender gender);
    }
}
