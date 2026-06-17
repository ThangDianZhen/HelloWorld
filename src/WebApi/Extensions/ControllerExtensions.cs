using System.Net;
using Application.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Response;

namespace WebApi.Extensions;

public static class ControllerExtensions
{
    public static IActionResult CreateResponse<T>(
        this ControllerBase controller,
        Result<T> result)
    {
        var response = ApiResponse<T>.FromResult(result);

        if (response.IsSuccess)
            return controller.Ok(response);

        return controller.StatusCode(
            (int)(result.StatusCode ?? HttpStatusCode.BadRequest),
            response);
    }
}
