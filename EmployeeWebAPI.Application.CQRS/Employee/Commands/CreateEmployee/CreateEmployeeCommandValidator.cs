using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {

        public CreateEmployeeCommandValidator()
        {
            RuleFor(c => c.Pesel.Value)
                 .Length(11)
                 .WithMessage("{PropertyName} length should be exactly 11 char long!}");

            RuleFor(c => c.Name.First)
               .Must(x=>x.Length>0 &&x.Length<=25)
               .WithMessage("{PropertyName} length should be between 1-25 characters!}");

            RuleFor(c => c.Name.Last)
              .Must(x => x.Length > 0 && x.Length <= 50)
             .WithMessage("{PropertyName} length should be between 1-50 characters!}");
        }
      
    }
}
