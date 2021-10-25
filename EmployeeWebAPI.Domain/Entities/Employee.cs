using EmployeeWebAPI.Domain.DDD;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Domain.Entities
{
    public class Employee : Entity<EmployeeId>
    {
        public Name Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Pesel Pesel { get; set; }
        public RegistrationNumber RegistrationNumber { get; set; }
        public Gender Gender { get; set; }

        public Employee()
        {
            if(RegistrationNumber==null)
                RegistrationNumber = new RegistrationNumber();
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


    }
}
