using EmployeeWebAPI.Domain.DDD;

namespace EmployeeWebAPI.Domain.ValueObjects.Ids
{
    public abstract class BaseId<T> : ValueObject<T> where T : ValueObject<T>
    {
        public BaseId()
        {

        }
    }
}
