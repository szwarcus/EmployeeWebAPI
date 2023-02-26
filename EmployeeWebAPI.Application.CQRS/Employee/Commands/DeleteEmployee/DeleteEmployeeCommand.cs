using EmployeeWebAPI.Domain.ValueObjects.Ids;
using MediatR;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<DeleteEmployeeCommandResponse>
    {
        public EmployeeId Id { get; set; }
    }
}
