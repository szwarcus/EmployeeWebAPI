using System;
using System.Collections.Generic;
using System.Text;

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
