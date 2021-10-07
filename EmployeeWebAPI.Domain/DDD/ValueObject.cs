using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeWebAPI.Domain.DDD
{
    public abstract class ValueObject<T> where T : ValueObject<T> 
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public virtual bool Equals(T obj)
        {
            if(obj!=null)
            {
                return GetEqualityComponents().SequenceEqual(obj.GetEqualityComponents());
            }

            return false;
        }

        public static bool operator ==(ValueObject<T> first, ValueObject<T> second) => Equals(first, second);
        public static bool operator !=(ValueObject<T> first, ValueObject<T> second) => !(first == second);
    }
}
