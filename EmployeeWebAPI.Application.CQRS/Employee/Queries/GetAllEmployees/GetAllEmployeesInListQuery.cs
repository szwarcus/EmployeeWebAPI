using MediatR;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees
{

    public class GetAllEmployeesInListQuery : IRequest<GetAllEmployeesQueryResponse>
    {
    }
}
