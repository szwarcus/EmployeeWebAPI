using EmployeeWebAPI.Application.Common;
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using EmployeeWebAPI.Domain.Status;
using FluentValidation.Results;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandResponse : BaseResponse
    {
        public IdDto EmployeeId { get; set; }


        public CreateEmployeeCommandResponse(ExecutionStatus status, string message) : base(status, message)
        {

        }

        public CreateEmployeeCommandResponse(ValidationResult validationResult)
           : base(validationResult)
        { }


        public CreateEmployeeCommandResponse(IdDto employeeIdDto) : base()
        {
            EmployeeId = employeeIdDto;
        }
    }
}

