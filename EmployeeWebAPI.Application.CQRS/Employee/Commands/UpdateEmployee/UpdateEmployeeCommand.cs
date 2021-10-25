using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee
{

    public class UpdateEmployeeCommand : IRequest<UpdateEmployeeCommandResponse>
    {
        public IdDto EmployeeId { get; set; }
        public NameDto Name { get; set; }
        public PeselDto Pesel { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public RegistrationNumberDto RegistrationNumber { get; set; }

    }
}
