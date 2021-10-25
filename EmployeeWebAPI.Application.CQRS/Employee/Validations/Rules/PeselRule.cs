using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Employee.Validations.Rules
{
    public class PeselRule : AbstractValidator<PeselDto>
    {
        public PeselRule()
        {
            RuleFor(c => c.Value)
               .Length(11)
               .WithMessage("{PropertyName} length should be exactly 11 char long!}");

            RuleFor(c =>c.Value)
                .Must(x => ValidatePesel(x))
                .WithMessage("{PropertyName} is incorrect!");

        }

        private static bool ValidatePesel(string input)
        {
            int[] weights = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            bool result = false;
            if (input.Length == 11)
            {
                int controlSum = CalculateControlSum(input, weights);
                int controlNum = controlSum % 10;
                controlNum = 10 - controlNum;
                if (controlNum == 10)
                {
                    controlNum = 0;
                }
                int lastDigit = int.Parse(input[input.Length - 1].ToString());
                result = controlNum == lastDigit;
            }
            return result;
        }

        private static int CalculateControlSum(string input, int[] weights, int offset = 0)
        {
            int controlSum = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                controlSum += weights[i + offset] * int.Parse(input[i].ToString());
            }
            return controlSum;
        }
    }
}
