
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Employee.Validations.Rules
{
    public class NameRule : AbstractValidator<NameDto>
    {
        public NameRule()
        {
            RuleFor(c => c.First)
               .Must(x => x.Length > 0 && x.Length <= 25)
               .WithMessage("{PropertyName} length should be between 1-25 characters!}");

            RuleFor(c => c.Last)
              .Must(x => x.Length > 0 && x.Length <= 50)
             .WithMessage("{PropertyName} length should be between 1-50 characters!}");
        }
    }
}
