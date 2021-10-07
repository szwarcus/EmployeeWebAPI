using EmployeeWebAPI.Domain.DDD;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmployeeWebAPI.Domain.ValueObjects
{
    public class RegistrationNumber : ValueObject<RegistrationNumber>
    {
        private static int nextId;
        private const string format = "D8";
        public string Value { get; private set; }
        

        public RegistrationNumber()
        {
            Value = Interlocked.Increment(ref nextId).ToString(format);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
