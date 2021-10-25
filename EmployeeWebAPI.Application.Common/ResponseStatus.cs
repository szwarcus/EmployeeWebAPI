namespace EmployeeWebAPI.Application.Common
{
    public enum ResponseStatus
    {
        Success=0,
        NotFound,
        DatabaseError,
        BusinessLogicError,
        InvalidQuery
    }
}
