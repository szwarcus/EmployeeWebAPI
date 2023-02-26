using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Domain.Entities;
using EmployeeWebAPI.Domain.Status;
using EmployeeWebAPI.Domain.ValueObjects;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeWebApi.Infrastracture.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeesDbContext _employeesDbContext;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(EmployeesDbContext employees, ILogger<EmployeeRepository> logger)
        {
            _employeesDbContext = employees;
            _logger = logger;
        }

        public async Task<ExecutionStatus<EmployeeId>> Add(Employee entity)
        {
            try
            {
                var record = await _employeesDbContext.Employees.AddAsync(entity);

                await _employeesDbContext.SaveChangesAsync();

                return ExecutionStatus<EmployeeId>.SuccessfulDatabase(record.Entity.Id);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error while trying to add {Aggregate}", nameof(Employee));

                return ExecutionStatus<EmployeeId>.ErrorDatabaseError($"Error while trying to add {nameof(Employee)}", ex);
            }
        }

        public async Task<ExecutionStatus<IEnumerable<Employee>>> List()
        {
            try
            {
                var records = await _employeesDbContext.Employees.ToListAsync();

                return ExecutionStatus<IEnumerable<Employee>>.SuccessfulDatabase(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to list all {Aggregate}", nameof(Employee));

                return ExecutionStatus<IEnumerable<Employee>>.ErrorDatabaseError($"Error while trying to list all {nameof(Employee)}", ex);
            }
        }

        public async Task<ExecutionStatus<Employee>> Get(EmployeeId id)
        {
            try
            {
                var record = await _employeesDbContext.Employees.FindAsync(id);

                return ExecutionStatus<Employee>.SuccessfulDatabase(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to search {Aggregate} with {Id}", nameof(Employee), id);

                return ExecutionStatus<Employee>.ErrorDatabaseError($"Error while trying to search {nameof(Employee)} with {id.Value}", ex);
            }
        }

        public async Task<ExecutionStatus<bool>> PeselExists(Pesel pesel)
        {
            try
            {
                var isFound = await _employeesDbContext.Employees.AnyAsync(employee => employee.Pesel.Value == pesel.Value);

                return ExecutionStatus<bool>.SuccessfulDatabase(isFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to search {Aggregate} with {Pesel}", nameof(Employee), pesel.Value);

                return ExecutionStatus<bool>.ErrorDatabaseError($"Error while trying to search {nameof(Pesel)} with {pesel.Value}", ex);
            }
        }

        public async Task<ExecutionStatus> Remove(EmployeeId id)
        {
            try
            {
                var record = await _employeesDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

                if (record is null)
                    return ExecutionStatus.ErrorDatabaseRecordNotFound($"Record with {id.Value} not found");

                _employeesDbContext.Employees.Remove(record);
                await _employeesDbContext.SaveChangesAsync();

                return ExecutionStatus.SuccessfulLogic();
            }
            catch (DbUpdateException ex)
            {
                var message = $"Error while trying to remove Employee with Id {id.Value}";
                _logger.LogError(ex, message, nameof(Employee));

                return ExecutionStatus.ErrorDatabaseError($"Failed to remove Employee with Id {id.Value}", ex);
            }
            catch (Exception ex)
            {
                var message = $"Error while trying to remove Employee with Id {id.Value}";
                _logger.LogError(ex, message, nameof(Employee));

                return ExecutionStatus.ErrorLogic($"Failed to remove Employee with Id {id.Value}", ex);
            }
        }

        public async Task<ExecutionStatus> Update(Employee employee)
        {
            try
            {
                var existingEmployee = await _employeesDbContext.Employees.FindAsync(employee.Id);

                if (existingEmployee == null)
                    return ExecutionStatus.ErrorDatabaseRecordNotFound($"Employee with id {employee.Id.Value} not found");

                // Update the value objects associated with the properties that can be updated
                existingEmployee.UpdateData(employee);

                // Save changes to the database
                await _employeesDbContext.SaveChangesAsync();

                return ExecutionStatus.SuccessfulDatabase();
            }
            catch (DbUpdateException ex)
            {
                var message = $"Error while trying to update Employee with Id {employee.Id.Value}";
                _logger.LogError(ex, message, nameof(Employee));

                return ExecutionStatus.ErrorDatabaseError($"Failed to remove Employee with Id {employee.Id.Value}", ex);
            }
            catch (Exception ex)
            {
                var message = $"Error while trying to remove Employee with Id {employee.Id.Value}";
                _logger.LogError(ex, message, nameof(Employee));

                return ExecutionStatus.ErrorLogic($"Failed to remove Employee with Id {employee.Id.Value}", ex);
            }
        }
    }
}
