using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using MediatR;


namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<DeleteEmployeeCommandResponse>
    {
        public IdDto EmployeeId { get; set; }
    }
}
