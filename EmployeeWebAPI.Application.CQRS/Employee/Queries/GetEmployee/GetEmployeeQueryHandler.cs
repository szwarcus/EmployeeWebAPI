using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee
{
    //TODO
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeQueryResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository,
                                       IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<GetEmployeeQueryResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var getEmployeeByIdAsync = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);

            if(getEmployeeByIdAsync.Success)
            {
                var employeeVM = _mapper.Map<EmployeeViewModel>(getEmployeeByIdAsync.ReturnValue);
                return new GetEmployeeQueryResponse(employeeVM);
            }

            return new GetEmployeeQueryResponse(getEmployeeByIdAsync.RemoveGeneric(), "GetEmployeeQuery - GetByIdAsync error");
        }
    }
}
