using System;
using System.Collections.Generic;
using System.Text;
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.Enums;
using MediatR;


namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommand: IRequest<CreateEmployeeCommandResponse>
    {
        public NameDto Name { get; set; }
        public DateTime BirthDate { get; set; }
        public PeselDto Pesel { get; set; }
        public Gender Gender { get; set; }
    }
}
