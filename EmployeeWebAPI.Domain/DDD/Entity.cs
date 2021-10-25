namespace EmployeeWebAPI.Domain.DDD
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }
    }
}
