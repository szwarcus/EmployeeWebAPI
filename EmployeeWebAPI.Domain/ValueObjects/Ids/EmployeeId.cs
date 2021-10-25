using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Domain.ValueObjects.Ids
{
    public class EmployeeId : BaseId<EmployeeId>
    {

        public Guid Value { get; set; }
        public EmployeeId()
        {
            Value = Guid.NewGuid();
        }
        public EmployeeId(Guid value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static EmployeeId NewUniqueId()
        {
            return new EmployeeId();
        }


    }
}
