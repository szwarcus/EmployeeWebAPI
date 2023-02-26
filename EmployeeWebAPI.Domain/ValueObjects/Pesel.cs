using EmployeeWebAPI.Domain.DDD;
using System.Collections.Generic;

namespace EmployeeWebAPI.Domain.ValueObjects
{
    public class Pesel : ValueObject<Pesel>
    {
        public string Value { get; private set; }

        public Pesel(string value) => Value = value;
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
