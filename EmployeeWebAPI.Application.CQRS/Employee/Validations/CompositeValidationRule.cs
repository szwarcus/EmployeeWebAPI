using FluentValidation;
using FluentValidation.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace EmployeeWebAPI.Application.CQRS.Employee.Validations
{
    public class CompositeValidationRule<T> : IValidationRule where T : class
    {
        private readonly IValidator[] _validators;

        public CompositeValidationRule(params IValidator[] validators)
        {
            if(validators == null)
            {
                throw new ArgumentException("validators");
            }
            _validators = validators;
        }

        public IEnumerable<IRuleComponent> Components => throw new NotImplementedException();

        public string[] RuleSets { get; set; }

        public string PropertyName { get; set; }

        public MemberInfo Member => throw new NotImplementedException();

        public Type TypeToValidate => throw new NotImplementedException();

        public bool HasCondition => throw new NotImplementedException();

        public bool HasAsyncCondition => throw new NotImplementedException();

        public LambdaExpression Expression => throw new NotImplementedException();

        public IEnumerable<IValidationRule> DependentRules => throw new NotImplementedException();
        public IEnumerable<FluentValidation.Results.ValidationFailure> Validate(FluentValidation.ValidationContext<T> context)
        {
            var validationFailures = new List<FluentValidation.Results.ValidationFailure>();
            foreach(var v in _validators)
            {
                var errors = v.Validate(context).Errors;
                validationFailures.AddRange(errors);
            }
            return validationFailures;
        }
        public string GetDisplayName(IValidationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
