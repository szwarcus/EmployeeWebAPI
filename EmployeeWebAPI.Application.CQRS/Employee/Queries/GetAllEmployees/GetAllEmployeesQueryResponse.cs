using EmployeeWebAPI.Application.Common;
using EmployeeWebAPI.Domain.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetAllEmployees
{ 
    public class GetAllEmployeesQueryResponse : BaseResponse
    {
        public List<EmployeesInListViewModel> EmployeeList { get; }
        public GetAllEmployeesQueryResponse(List<EmployeesInListViewModel> list) : base()
        {
            EmployeeList = list;
        }

        public GetAllEmployeesQueryResponse(ExecutionStatus status) : base(status)
        { }



        public GetAllEmployeesQueryResponse(ExecutionStatus status, string message) : base(status, message)
        {

        }
    }
}
