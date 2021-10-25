using EmployeeWebAPI.Application.CQRS.Employee.Validations.Rules;
using FluentValidation;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {

        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .SetValidator(new NameRule());

            RuleFor(x=>x.Pesel)
                .NotNull()
                .SetValidator(new PeselRule());
        }

     
      
    }
}
