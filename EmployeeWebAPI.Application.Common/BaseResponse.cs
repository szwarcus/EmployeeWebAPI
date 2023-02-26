using EmployeeWebAPI.Domain.Status;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace EmployeeWebAPI.Application.Common
{
    public abstract class BaseResponse
    {
        public ResponseStatus Status { get; set; }

        public bool Success { get; set;}
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        protected BaseResponse()
        {
            Errors = new List<string>();
            Status = ResponseStatus.Success;
            Success = true;
        }

        protected BaseResponse(ExecutionStatus status, string message=null)
        {
            Errors = new List<string>();
            if (!status.Success)
            {
                Success = false;

                ComputeStatus(status);

                Message = message;
                Message += status.Message;
            }
            else
            {
                Success = true;
                Status = ResponseStatus.Success;
            }
        }

        private void ComputeStatus(ExecutionStatus status)
        {
            Status = (status.Source, status.Reason) switch
            {
                (Source.ApplicationLogic, _) => ResponseStatus.BusinessLogicError,
                (Source.Database, Reason.DuplicatedUniqueId) => ResponseStatus.InvalidQuery,
                (Source.Database, Reason.NotFoundInDb) => ResponseStatus.NotFound,
                (Source.Database, Reason.Error) => ResponseStatus.BusinessLogicError,
                _ => throw new NotImplementedException(),
            };
        }

        protected BaseResponse(string message = null)
        {
            Errors = new List<string>();
            Status = ResponseStatus.Success;
            Success = true;
            Message = message;
        }
        protected BaseResponse(ResponseStatus status)
        {
            Errors = new List<string>();
            Status = status;
            Success = status != ResponseStatus.Success;
        }



        protected BaseResponse(string message, bool success)
        {
            Errors = new List<string>();
            Success = success;
            Message = message;
        }

        protected BaseResponse(ValidationResult validationResult)
        {
            Errors = new List<String>();
            Success = validationResult.Errors.Count < 0;
            foreach (var item in validationResult.Errors)
            {
                Errors.Add(item.ErrorMessage);
            }

            if (!Success)
                Status = ResponseStatus.BusinessLogicError;
            else
                Status = ResponseStatus.Success;
        }
    }
}
