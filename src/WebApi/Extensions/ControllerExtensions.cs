using System.Net;
using Ignite.Application.Results;
using Ignite.WebApi.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace Ignite.WebApi.Extensions;

public static class ControllerExtensions
{
    public static IActionResult CreateResponse<T>(this ControllerBase controller, Result<T> result)
    {
        var response = ApiResponse<T>.FromResult(result);
        if (response.IsSuccess)
        {
            var status = (int)(result.StatusCode ?? HttpStatusCode.OK);
            return controller.StatusCode(status, response);
        }

        return controller.StatusCode((int)(result.StatusCode ?? HttpStatusCode.BadRequest), response);
    }
}
