using EmployeeWebAPI.Domain.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Domain.ValueObjects.Ids
{
    public abstract class BaseId<T> : ValueObject<T> where T : ValueObject<T>
    {
        public BaseId()
        {

        }
    }
}
