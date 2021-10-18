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
        Task<ExecutionStatus<T>> GetByIdAsync(int id);

        Task<ExecutionStatus<int>> UpdateAsync(T entity);
        Task<ExecutionStatus<int>> RemoveByIdAsync(int id);
    }
}
