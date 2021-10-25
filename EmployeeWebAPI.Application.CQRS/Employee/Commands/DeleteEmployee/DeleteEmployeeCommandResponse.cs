using EmployeeWebAPI.Application.Common;
using EmployeeWebAPI.Domain.Status;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandResponse : BaseResponse
    {
        public DeleteEmployeeCommandResponse() : base()
        {
        }

        public DeleteEmployeeCommandResponse(ExecutionStatus status, string message) : base(status, message)
        {

        }
    }
}
