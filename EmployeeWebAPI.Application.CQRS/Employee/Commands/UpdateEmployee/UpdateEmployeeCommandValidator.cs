using EmployeeWebAPI.Application.CQRS.Employee.Validations.Rules;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee
{

    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .SetValidator(new NameRule());

            RuleFor(x => x.Pesel)
                .NotNull()
                .SetValidator(new PeselRule());
        }
    }
}
