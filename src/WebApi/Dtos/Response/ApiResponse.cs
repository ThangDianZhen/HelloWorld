using System.Net;
using Ignite.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace Ignite.WebApi.Dtos.Response;

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string ErrorHeader { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = [];
    public List<string> Warnings { get; set; } = [];
    public string CorrelationId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public static ApiResponse<T> FromResult(Result<T> result, string? correlationId = null)
        => new()
        {
            IsSuccess = result.IsSuccess,
            Data = result.Data,
            ErrorHeader = result.ErrorHeader,
            Errors = result.Errors,
            Warnings = result.Warnings,
            Message = result.Message,
            CorrelationId = correlationId ?? string.Empty
        };
}
