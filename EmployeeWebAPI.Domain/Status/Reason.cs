namespace EmployeeWebAPI.Domain.Status
{
    public enum Reason
    {
        None,
        Error,
        UnhandledException,
        NotFoundInDb,
        DuplicatedUniqueId
    }
}
