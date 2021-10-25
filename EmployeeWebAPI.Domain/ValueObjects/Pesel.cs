using EmployeeWebAPI.Domain.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Domain.ValueObjects
{
    public class Pesel : ValueObject<Pesel>
    {
        public string Value { get; private set; }

        public Pesel(string value)
        {
            //if (string.IsNullOrEmpty(value) || value.Length != 11)
            //    throw new ArgumentException("Pesel should be 11 char long and cannot be empty");
            Value = value;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
