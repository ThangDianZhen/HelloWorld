using Application.Results;

namespace WebApi.Dtos.Response;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string ErrorHeader { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
    public List<string> Warnings { get; set; } = new();
    public string CorrelationId { get; set; } = string.Empty;

    public static ApiResponse<T> Success(
        T data,
        string? correlationId = null,
        IEnumerable<string>? warnings = null)
        => new()
        {
            IsSuccess = true,
            Data = data,
            CorrelationId = correlationId ?? string.Empty,
            Warnings = warnings?.ToList() ?? new()
        };

    public static ApiResponse<T> Failure(
        string message,
        string? correlationId = null,
        IEnumerable<string>? errors = null)
        => new()
        {
            IsSuccess = false,
            CorrelationId = correlationId ?? string.Empty,
            ErrorHeader = message,
            Errors = errors?.ToList() ?? new()
        };

    public static ApiResponse<T> FromResult(Result<T> result, string? correlationId = null)
        => new()
        {
            IsSuccess = result.IsSuccess,
            Data = result.Data,
            ErrorHeader = result.Message,
            Errors = result.Errors,
            Warnings = result.Warnings,
            CorrelationId = correlationId ?? string.Empty
        };
}
