using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HR.LeaveManagement.Application.Responses
{
    public class BaseCommandResponse
    {
        private const string DefaultSuccessMessage = "request processed successfully.";
        private const string DefaultFailMessage = "request failed to process.";

        public int Id { get; set; }
        public bool IsSuccess { get; set; }

        private readonly string _message;
        public string Message => _message;

        public List<string>? Errors { get; set; }

        private BaseCommandResponse(int id, bool isSuccess, string? message, List<string>? errors)
        {
            Id = id;
            IsSuccess = isSuccess;
            _message = message ?? (IsSuccess ? DefaultSuccessMessage : DefaultFailMessage);
            Errors = errors;
        }
#pragma warning disable IDE0090
        public static BaseCommandResponse Success(int id, string? message = null) =>
            new BaseCommandResponse(id, isSuccess: true, message, errors: null);

        public static BaseCommandResponse Success(string? message = null) =>
            new BaseCommandResponse(id: 0, isSuccess: true, message, errors: null);

        public static BaseCommandResponse Fail(List<string> errors, string? message = null) =>
            new BaseCommandResponse(id: 0, isSuccess: false, message, errors: errors);

        public static BaseCommandResponse Fail(string error, string? message = null) =>
            new BaseCommandResponse(id: 0, isSuccess: false, message, errors: [error]);
#pragma warning restore IDE0090
    }
}
