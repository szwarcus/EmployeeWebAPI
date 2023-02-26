using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Status;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Application.Contracts.Persistence
{
    public interface IEmployeeRepository
    {
        Task<ExecutionStatus<bool>> PeselExists(Pesel pesel);

        Task<ExecutionStatus<EmployeeId>> Add(Employee entity);

        Task<ExecutionStatus<Employee>> Get(EmployeeId id);

        Task<ExecutionStatus> Update(Employee employee);

        Task<ExecutionStatus<IEnumerable<Employee>>> List();

        Task<ExecutionStatus> Remove(EmployeeId id);

 
    }

}
