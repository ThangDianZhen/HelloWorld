using Ignite.Application.Dto.Application;
using Ignite.Application.Interfaces.Services.Application;
using Ignite.Application.Interfaces.Services.Entities;
using Ignite.Core.Constants;
using Ignite.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ignite.WebApi.Controllers;

[ApiController]
[Route("api/site")]
public class SiteController : ControllerBase
{
    private readonly IChurchSiteService _churchSiteService;

    public SiteController(IChurchSiteService churchSiteService)
    {
        _churchSiteService = churchSiteService;
    }

    [HttpGet("home")]
    public async Task<IActionResult> Home(CancellationToken cancellationToken)
        => this.CreateResponse(await _churchSiteService.GetHomeAsync(cancellationToken));
}

[ApiController]
[Route("api/campuses")]
public class CampusesController : ControllerBase
{
    private readonly ICampusService _campusService;

    public CampusesController(ICampusService campusService)
    {
        _campusService = campusService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool publishedOnly = true, CancellationToken cancellationToken = default)
        => this.CreateResponse(await _campusService.GetAllAsync(publishedOnly, cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _campusService.GetByIdAsync(id, cancellationToken));

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
        => this.CreateResponse(await _campusService.GetBySlugAsync(slug, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CampusWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _campusService.CreateAsync(dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CampusWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _campusService.UpdateAsync(id, dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _campusService.DeleteAsync(id, cancellationToken));
}

[ApiController]
[Route("api/sermons")]
public class SermonsController : ControllerBase
{
    private readonly ISermonService _sermonService;

    public SermonsController(ISermonService sermonService)
    {
        _sermonService = sermonService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLatest([FromQuery] int take = 20, [FromQuery] bool publishedOnly = true, CancellationToken cancellationToken = default)
        => this.CreateResponse(await _sermonService.GetLatestAsync(take, publishedOnly, cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _sermonService.GetByIdAsync(id, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SermonWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _sermonService.CreateAsync(dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] SermonWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _sermonService.UpdateAsync(id, dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _sermonService.DeleteAsync(id, cancellationToken));
}

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool publishedOnly = true, CancellationToken cancellationToken = default)
        => this.CreateResponse(await _eventService.GetAllAsync(publishedOnly, cancellationToken));

    [HttpGet("upcoming")]
    public async Task<IActionResult> GetUpcoming([FromQuery] bool publishedOnly = true, CancellationToken cancellationToken = default)
        => this.CreateResponse(await _eventService.GetUpcomingAsync(publishedOnly, cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _eventService.GetByIdAsync(id, cancellationToken));

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
        => this.CreateResponse(await _eventService.GetBySlugAsync(slug, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ChurchEventWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _eventService.CreateAsync(dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ChurchEventWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _eventService.UpdateAsync(id, dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _eventService.DeleteAsync(id, cancellationToken));
}

[ApiController]
[Route("api/pages")]
public class PagesController : ControllerBase
{
    private readonly IPageService _pageService;

    public PagesController(IPageService pageService)
    {
        _pageService = pageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool publishedOnly = true, CancellationToken cancellationToken = default)
        => this.CreateResponse(await _pageService.GetAllAsync(publishedOnly, cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _pageService.GetByIdAsync(id, cancellationToken));

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
        => this.CreateResponse(await _pageService.GetBySlugAsync(slug, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PageWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _pageService.CreateAsync(dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PageWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _pageService.UpdateAsync(id, dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _pageService.DeleteAsync(id, cancellationToken));
}

[ApiController]
[Route("api/ministries")]
public class MinistriesController : ControllerBase
{
    private readonly IMinistryService _ministryService;

    public MinistriesController(IMinistryService ministryService)
    {
        _ministryService = ministryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool publishedOnly = true, CancellationToken cancellationToken = default)
        => this.CreateResponse(await _ministryService.GetAllAsync(publishedOnly, cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _ministryService.GetByIdAsync(id, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MinistryWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _ministryService.CreateAsync(dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MinistryWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _ministryService.UpdateAsync(id, dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _ministryService.DeleteAsync(id, cancellationToken));
}

[ApiController]
[Route("api/connect")]
public class ConnectController : ControllerBase
{
    private readonly IConnectRequestService _connectRequestService;

    public ConnectController(IConnectRequestService connectRequestService)
    {
        _connectRequestService = connectRequestService;
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] ConnectRequestWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _connectRequestService.SubmitAsync(dto, cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => this.CreateResponse(await _connectRequestService.GetAllAsync(cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPost("{id:guid}/read")]
    public async Task<IActionResult> MarkRead(Guid id, CancellationToken cancellationToken)
        => this.CreateResponse(await _connectRequestService.MarkReadAsync(id, cancellationToken));
}

[ApiController]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    private readonly IChurchSettingsService _settingsService;

    public SettingsController(IChurchSettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
        => this.CreateResponse(await _settingsService.GetAsync(cancellationToken));

    [Authorize(Roles = Roles.Admin)]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ChurchSettingsWriteDto dto, CancellationToken cancellationToken)
        => this.CreateResponse(await _settingsService.UpdateAsync(dto, cancellationToken));
}
