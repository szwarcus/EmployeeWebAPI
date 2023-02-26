using EmployeeWebAPI.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand,DeleteEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository) =>        
            _employeeRepository = employeeRepository;
        
        public async Task<DeleteEmployeeCommandResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result =await _employeeRepository.Remove(request.Id);

            return new DeleteEmployeeCommandResponse(result,"DeleteEmployeeCommandHandler") ;
        }
    }
}
