using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Domain.ValueObjects.Ids
{
    public class EmployeeId : BaseId<EmployeeId>
    {

        public int Value { get; set; }

        public EmployeeId(int value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        
    }
}
