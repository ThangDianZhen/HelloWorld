using System.Net;

namespace Ignite.Application.Results;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public HttpStatusCode? StatusCode { get; set; } = HttpStatusCode.OK;
    public string ErrorHeader { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = [];
    public bool HasErrors => Errors.Count > 0;
    public bool IsSingleError => Errors.Count == 1;
    public List<string> Warnings { get; set; } = [];
    public bool HasWarnings => Warnings.Count > 0;
    public string ErrorMessage => Errors.Count > 0 ? Errors[0] : string.Empty;

    public static Result<T> Success(
        T data,
        string message = "",
        IEnumerable<string>? warnings = null,
        HttpStatusCode statusCode = HttpStatusCode.OK)
        => new()
        {
            IsSuccess = true,
            Data = data,
            Message = message,
            StatusCode = statusCode,
            Warnings = warnings?.ToList() ?? []
        };

    public static Result<T> Failure(
        string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new()
        {
            IsSuccess = false,
            ErrorHeader = errorMessage,
            Errors = [errorMessage],
            StatusCode = statusCode
        };

    public static Result<T> Failure(
        string errorHeader,
        IEnumerable<string> errorMessages,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new()
        {
            IsSuccess = false,
            ErrorHeader = errorHeader,
            Errors = errorMessages.ToList(),
            StatusCode = statusCode
        };
}
