using EmployeeWebAPI.Application.Common;
using EmployeeWebAPI.Domain.Status;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using FluentValidation.Results;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandResponse : BaseResponse
    {
        public EmployeeId EmployeeId { get; set; }


        public CreateEmployeeCommandResponse(ExecutionStatus status, string message=null) : base(status, message)
        {

        }

        public CreateEmployeeCommandResponse(ValidationResult validationResult)
           : base(validationResult)
        { }


        public CreateEmployeeCommandResponse(EmployeeId employeeId) : base()
        {
            EmployeeId = employeeId;
        }
    }
}

