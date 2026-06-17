using Application.Interfaces.Services.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GreetingController : ControllerBase
{
    private readonly IGreetingService _greetingService;
    private readonly ILogger<GreetingController> _logger;

    public GreetingController(
        IGreetingService greetingService,
        ILogger<GreetingController> logger)
    {
        _greetingService = greetingService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _greetingService.GetAllAsync(cancellationToken);
        return this.CreateResponse(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGreetingRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _greetingService.CreateGreetingAsync(request.Name, cancellationToken);
        return this.CreateResponse(result);
    }
}

public record CreateGreetingRequest(string Name);
