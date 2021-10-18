using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Status;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Application.Contracts.Persistence
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
        Task<ExecutionStatus<bool>> PeselExists(Pesel pesel);

        Task<ExecutionStatus<EmployeeId>> AddAsync(Employee entity);
    }
}
