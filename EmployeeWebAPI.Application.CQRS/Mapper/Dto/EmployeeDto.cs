﻿using EmployeeWebAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Mapper.Dto
{
    public class EmployeeDto
    {
        public Guid Id {get;set;}
        public NameDto Name { get; set; } 
        public DateTime BirthDate { get; set; }
        public PeselDto Pesel { get; set; }
        public RegistrationNumberDto RegistrationNumber { get; set; }
        public Gender Gender { get; set; }
    }
}
