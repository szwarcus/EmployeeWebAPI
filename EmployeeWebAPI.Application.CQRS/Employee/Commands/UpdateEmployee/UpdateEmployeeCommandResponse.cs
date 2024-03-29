﻿using EmployeeWebAPI.Application.Common;
using EmployeeWebAPI.Domain.Status;
using FluentValidation.Results;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee
{

    public class UpdateEmployeeCommandResponse : BaseResponse
    {
        public UpdateEmployeeCommandResponse() : base()
        { }
        public UpdateEmployeeCommandResponse(ValidationResult validationResult): base(validationResult)
        { }
        public UpdateEmployeeCommandResponse(ExecutionStatus status, string message) : base(status, message)
        { }
    }
}
