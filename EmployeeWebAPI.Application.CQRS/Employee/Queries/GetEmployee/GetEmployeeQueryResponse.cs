using EmployeeWebAPI.Application.Common;
using EmployeeWebAPI.Domain.Status;

namespace EmployeeWebAPI.Application.CQRS.Employee.Queries.GetEmployee
{
    //TODO
    public class GetEmployeeQueryResponse : BaseResponse
    {
        public EmployeeViewModel Employee { get; }

        public GetEmployeeQueryResponse() : base()
        { }
        public GetEmployeeQueryResponse(EmployeeViewModel employee)
        : base()
        {
            Employee = employee;
        }

        public GetEmployeeQueryResponse(ExecutionStatus status, string message) : base(status,message)
        { }


    }
}
