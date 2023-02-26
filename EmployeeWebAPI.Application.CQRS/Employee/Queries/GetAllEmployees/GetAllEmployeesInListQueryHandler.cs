using AutoMapper;
using EmployeeWebAPI.Application.Contracts.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees
{

    public class GetAllEmployeesInListQueryHandler : IRequestHandler<GetAllEmployeesInListQuery, GetAllEmployeesQueryResponse>
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesInListQueryHandler(IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<GetAllEmployeesQueryResponse> Handle(GetAllEmployeesInListQuery request, 
            CancellationToken cancellationToken)
        {
            var getAllEmployeesAsync = await _employeeRepository.List();

            if (!getAllEmployeesAsync.Success) 
                return new GetAllEmployeesQueryResponse(getAllEmployeesAsync.RemoveGeneric());
           
            var getAllEmployeesAsyncVM = _mapper.Map<List<EmployeesInListViewModel>>(getAllEmployeesAsync.ReturnValue);

            return new GetAllEmployeesQueryResponse(getAllEmployeesAsyncVM);
        }
    }
}
