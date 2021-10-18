using System;
using System.Collections.Generic;
using System.Text;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.ValueObjects;
using MediatR;


namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommand: IRequest<CreateEmployeeCommandResponse>
    {
        public Name Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Pesel Pesel { get; set; }
        public Gender Gender { get; set; }
    }
}
