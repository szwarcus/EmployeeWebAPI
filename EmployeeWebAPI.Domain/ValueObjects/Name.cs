using EmployeeWebAPI.Domain.DDD;
using System;
using System.Collections.Generic;

namespace EmployeeWebAPI.Domain.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        public string First { get; private set; }

        public string Last { get; private set; }

        public Name(string first, string last)
        {
            if (string.IsNullOrEmpty(first) || first.Length > 25)
                throw new ArgumentException("First name should be at most 25 char long and cannot be empty");

            if (string.IsNullOrEmpty(last) || last.Length > 50)
                throw new ArgumentException("Last name should be at most 50 char long and cannot be empty");

            First = first;
            Last = last;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
        }
    }
}
