using EmployeeWebAPI.Domain.ValueObjects.Ids;
using MediatR;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee
{
    public class GetEmployeeQuery : IRequest<GetEmployeeQueryResponse>
    {
        public EmployeeId Id { get; set; }
    }
}
