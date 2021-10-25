using System;
using System.Collections.Generic;
using System.Text;

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
