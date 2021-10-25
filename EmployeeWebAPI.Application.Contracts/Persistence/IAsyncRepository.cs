using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeWebAPI.Domain.Status;

namespace EmployeeWebAPI.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<ExecutionStatus<IEnumerable<T>>> GetAllAsync();
        Task<ExecutionStatus<T>> GetByIdAsync(Guid id);

        Task<ExecutionStatus> UpdateAsync(T entity);
        Task<ExecutionStatus> RemoveByIdAsync(Guid id);
    }
}
