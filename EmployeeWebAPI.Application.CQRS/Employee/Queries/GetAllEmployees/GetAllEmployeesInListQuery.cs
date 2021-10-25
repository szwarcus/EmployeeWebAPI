using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees
{

    public class GetAllEmployeesInListQuery : IRequest<GetAllEmployeesQueryResponse>
    {
    }
}
