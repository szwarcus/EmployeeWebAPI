using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace EmployeeWebAPI.Application.CQRS.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeCommandResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
                                             IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task<CreateEmployeeCommandResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEmployeeCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
                return new CreateEmployeeCommandResponse(validatorResult);

            var peselAlreadyExist = await _employeeRepository.PeselExists(request.Pesel);

            if(!peselAlreadyExist.Success && peselAlreadyExist.ReturnValue==true)
            {
                return new CreateEmployeeCommandResponse(peselAlreadyExist.RemoveGeneric(),"Employee with this PESEL already exist");
            }

            var employee = _mapper.Map<CreateEmployeeCommand,Domain.Entities.Employee>(request);
            var runAsync = await _employeeRepository.AddAsync(employee);

            if(!runAsync.Success)
            {
                return new CreateEmployeeCommandResponse(runAsync.RemoveGeneric(), "");
            }

            var employeeIdDto = _mapper.Map<IdDto>(runAsync.ReturnValue);
            return new CreateEmployeeCommandResponse(employeeIdDto);
        }
    }
}
