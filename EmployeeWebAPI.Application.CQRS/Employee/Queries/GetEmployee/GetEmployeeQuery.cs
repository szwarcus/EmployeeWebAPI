using EmployeeWebAPI.Application.CQRS.Mapper.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee
{
    //TODO
    public class GetEmployeeQuery : IRequest<GetEmployeeQueryResponse>
    {
        public IdDto EmployeeId { get; set; }
    }
}
