using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Enums;
using System;

namespace EmployeeWebAPI.Domain.Factories
{
    public interface IEmployeeFactory
    {
        Employee CreateEmployee(string firstName, string lastName, string pesel, DateTime birthDate, Gender gender);
    }
}
