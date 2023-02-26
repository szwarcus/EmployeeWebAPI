using EmployeeWebAPI.Domain.DDD;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using System;

namespace EmployeeWebAPI.Domain.Entities
{
    public class Employee : Entity<EmployeeId>
    {
        public Name Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public Pesel Pesel { get; private set; }

        public RegistrationNumber RegistrationNumber { get; private set; }

        public Gender Gender { get; private set; }

        public Employee()
        {
            RegistrationNumber ??= new RegistrationNumber();
            Id = EmployeeId.NewUniqueId();
        }

        public Employee(Name name,
            DateTime birthDate,
            Pesel pesel,
            RegistrationNumber registrationNumber,
            Gender gender,
            EmployeeId employeeId)
        {
            Id = employeeId;
            Name = name;
            BirthDate = birthDate;
            Pesel = pesel;
            RegistrationNumber = registrationNumber;
            Gender = gender;
        }

        public void UpdateData(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            BirthDate = employee.BirthDate;
            Pesel = employee.Pesel;
            Gender = employee.Gender;
        }
    }
}
