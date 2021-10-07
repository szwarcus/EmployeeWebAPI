using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Domain.DDD
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }
    }
}
