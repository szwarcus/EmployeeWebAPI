using EmployeeWebAPI.Application.CQRS.Dto;
using EmployeeWebAPI.Domain.Enums;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using MediatR;
using System;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee
{

    public class UpdateEmployeeCommand : IRequest<UpdateEmployeeCommandResponse>
    {
        public EmployeeId EmployeeId { get; set; }
        public NameDto Name { get; set; }
        public PeselDto Pesel { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
