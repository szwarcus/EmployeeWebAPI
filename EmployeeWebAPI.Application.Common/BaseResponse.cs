using EmployeeWebAPI.Domain.Status;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

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

        protected BaseResponse(ExecutionStatus status, string message)
        {
            Errors = new List<string>();
            if (!status.Success)
            {
                Success = false;
                if (status.Source == Source.ApplicationLogic)
                    Status = ResponseStatus.BusinessLogicError;
                if (status.Source == Source.Database)
                    Status = ResponseStatus.DatabaseError;
                if (status.Source == Source.Database
                    && status.Reason == Reason.NotFoundInDb)
                    Status = ResponseStatus.NotFound;
                if (status.Source == Source.Database
                    && status.Reason == Reason.DuplicatedUniqueId)
                    Status = ResponseStatus.InvalidQuery;

                    Message = message;
                Message += status.Message;
            }
            else
            {
                Success = true;
                Status = ResponseStatus.Success;
            }
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
