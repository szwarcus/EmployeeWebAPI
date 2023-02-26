using System;

namespace EmployeeWebAPI.Domain.Status
{
    public class ExecutionStatus
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public Source Source { get; set; }

        public Reason Reason { get; set; }
        public static ExecutionStatus SuccessfulLogic()
        {
            return new ExecutionStatus()
            {
                Source = Source.ApplicationLogic,
                Success = true
            };
        }

        public static ExecutionStatus ErrorLogic(string message, Exception ex=null)
        {
            return new ExecutionStatus()
            {
                Message = message,
                Exception = ex,
                Source = Source.ApplicationLogic,
                Reason = Reason.UnhandledException,
                Success = false
            };
        }

        public static ExecutionStatus SuccessfulDatabase()
        {
            return new ExecutionStatus()
            {
                Source = Source.Database,
                Success = true
            };
        }

        public static ExecutionStatus ErrorDatabaseRecordNotFound(string message, Exception ex = null)
        {
            return new ExecutionStatus()
            {
                Message = message,
                Exception = ex,
                Source = Source.Database,
                Reason = Reason.NotFoundInDb,
                Success = false
            };
        }

        public static ExecutionStatus ErrorDatabaseError(string message, Exception ex)
        {
            return new ExecutionStatus()
            {
                Message = message,
                Exception = ex,
                Source = Source.Database,
                Reason = Reason.Error,
                Success = false
            };
        }
    }
    public class ExecutionStatus <T>
    {
        public bool Success { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
        public Source Source { get; set; }

        public Reason Reason { get; set; }
        public T ReturnValue { get; set; }

        public ExecutionStatus RemoveGeneric()
        {
            return new ExecutionStatus()
            {
                Success = this.Success,
                Exception = this.Exception,
                Message = this.Message,
                Reason = this.Reason,
                Source = this.Source
            };
        }
        public static ExecutionStatus<T> SuccessfulLogic(T value)
        {
            return new ExecutionStatus<T>()
            {
                Source = Source.ApplicationLogic,
                ReturnValue=value,
                Success = true
            };
        }

        public static ExecutionStatus<T> ErrorLogic(string message, Exception ex=null)
        {
            return new ExecutionStatus<T>()
            {
                Message=message,
                Exception=ex,
                Source = Source.ApplicationLogic,
                Reason = Reason.UnhandledException,
                Success = false
            };
        }

        public static ExecutionStatus<T> SuccessfulDatabase(T value)
        {
            return new ExecutionStatus<T>()
            {
                Source = Source.Database,
                ReturnValue = value,
                Success = true
            };
        }

        public static ExecutionStatus<T> ErrorDatabaseRecordNotFound(string message, Exception ex)
        {
            return new ExecutionStatus<T>()
            {
                Message = message,
                Exception = ex,
                Source = Source.Database,
                Reason = Reason.NotFoundInDb,
                Success = false
            };
        }

        public static ExecutionStatus<T> ErrorDatabaseError(string message, Exception ex)
        {
            return new ExecutionStatus<T>()
            {
                Message = message,
                Exception = ex,
                Source = Source.Database,
                Reason = Reason.Error,
                Success = false
            };
        }
    }
}
