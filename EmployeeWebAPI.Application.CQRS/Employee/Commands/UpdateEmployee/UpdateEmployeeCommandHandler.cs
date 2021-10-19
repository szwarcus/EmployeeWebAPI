using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.UpdateEmployee
{

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
                                            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEmployeeCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new UpdateEmployeeCommandResponse(validatorResult);


            var employeeAsync = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);
            if(employeeAsync.ReturnValue != null)
            {
                var employee = _mapper.Map<UpdateEmployeeCommand, Domain.Entities.Employee>(request);
                var updateEmployee = await _employeeRepository.UpdateAsync(employee);
                if (!updateEmployee.Success)
                {
                    return new UpdateEmployeeCommandResponse(updateEmployee, "UpdateEmployeeCommandHandler - UpdateAsync error");
                }

                return new UpdateEmployeeCommandResponse();
            }
            else
            {
                return new UpdateEmployeeCommandResponse(employeeAsync.RemoveGeneric(), "UpdateEmployeeCommandHandler - GetByIdAsync error");
            }
        }
    }
}
