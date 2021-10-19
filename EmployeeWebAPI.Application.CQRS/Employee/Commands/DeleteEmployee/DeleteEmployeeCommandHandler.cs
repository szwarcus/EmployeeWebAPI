using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Domain.Status;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand,DeleteEmployeeCommandResponse>
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository,
                                             IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;

        }

        public async Task<DeleteEmployeeCommandResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeId = _mapper.Map<EmployeeId>(request.EmployeeId);

            var result =await _employeeRepository.RemoveByIdAsync(employeeId.Value);

            return new DeleteEmployeeCommandResponse(result,"DeleteEmployeeCommandHandler") ;
        }
    }
}
