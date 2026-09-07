using System.Net;
using Ignite.Application.Dto.Application;
using Ignite.Application.Dto.Entities;
using Ignite.Application.Dto.Extensions;
using Ignite.Application.Exceptions;
using Ignite.Application.Helpers;
using Ignite.Application.Interfaces.Persistence.Repositories;
using Ignite.Application.Interfaces.Services.Entities;
using Ignite.Application.Results;
using Ignite.Core.Entities;

namespace Ignite.Application.Services.Entities;

public class CampusService : ICampusService
{
    private readonly ICampusRepository _repository;

    public CampusService(ICampusRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyList<CampusDto>>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var campuses = await _repository.GetAllAsync(publishedOnly, cancellationToken);
            return Result<IReadOnlyList<CampusDto>>.Success(campuses.Select(c => c.ToDto()).ToList());
        }
        catch (RepositoryException ex)
        {
            return Result<IReadOnlyList<CampusDto>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<CampusDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var campus = await _repository.GetByIdAsync(id, cancellationToken);
            return campus is null
                ? Result<CampusDto>.Failure("Campus not found.", HttpStatusCode.NotFound)
                : Result<CampusDto>.Success(campus.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<CampusDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<CampusDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        try
        {
            var campus = await _repository.GetBySlugAsync(slug, cancellationToken);
            return campus is null
                ? Result<CampusDto>.Failure("Campus not found.", HttpStatusCode.NotFound)
                : Result<CampusDto>.Success(campus.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<CampusDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<CampusDto>> CreateAsync(CampusWriteDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (validation is not null)
        {
            return validation;
        }

        try
        {
            var campus = new Campus();
            campus.Apply(dto, ResolveSlug(dto));
            SyncServiceTimes(campus, dto);
            await _repository.AddAsync(campus, cancellationToken);
            return Result<CampusDto>.Success(campus.ToDto(), statusCode: HttpStatusCode.Created);
        }
        catch (RepositoryException ex)
        {
            return Result<CampusDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<CampusDto>> UpdateAsync(Guid id, CampusWriteDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (validation is not null)
        {
            return validation;
        }

        try
        {
            var campus = await _repository.GetByIdAsync(id, cancellationToken);
            if (campus is null)
            {
                return Result<CampusDto>.Failure("Campus not found.", HttpStatusCode.NotFound);
            }

            campus.Apply(dto, ResolveSlug(dto));
            SyncServiceTimes(campus, dto);
            await _repository.UpdateAsync(campus, cancellationToken);
            return Result<CampusDto>.Success(campus.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<CampusDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var campus = await _repository.GetByIdAsync(id, cancellationToken);
            if (campus is null)
            {
                return Result<bool>.Failure("Campus not found.", HttpStatusCode.NotFound);
            }

            await _repository.DeleteAsync(campus, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (RepositoryException ex)
        {
            return Result<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    private static Result<CampusDto>? Validate(CampusWriteDto dto)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(dto.Name)) errors.Add("Name is required.");
        if (string.IsNullOrWhiteSpace(dto.City)) errors.Add("City is required.");
        if (string.IsNullOrWhiteSpace(dto.Country)) errors.Add("Country is required.");
        return errors.Count == 0 ? null : Result<CampusDto>.Failure("Campus is invalid.", errors);
    }

    private static string ResolveSlug(CampusWriteDto dto)
        => string.IsNullOrWhiteSpace(dto.Slug) ? SlugHelper.ToSlug(dto.Name) : SlugHelper.ToSlug(dto.Slug);

    private static void SyncServiceTimes(Campus campus, CampusWriteDto dto)
    {
        campus.ServiceTimes.Clear();
        foreach (var time in dto.ServiceTimes)
        {
            campus.ServiceTimes.Add(new ServiceTime
            {
                Day = time.Day,
                TimeLabel = time.TimeLabel.Trim(),
                Title = string.IsNullOrWhiteSpace(time.Title) ? "Main Service" : time.Title.Trim(),
                Notes = time.Notes,
                SortOrder = time.SortOrder
            });
        }
    }
}

public class SermonService : ISermonService
{
    private readonly ISermonRepository _repository;

    public SermonService(ISermonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyList<SermonDto>>> GetLatestAsync(int take, bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var sermons = await _repository.GetLatestAsync(take <= 0 ? 20 : take, publishedOnly, cancellationToken);
            return Result<IReadOnlyList<SermonDto>>.Success(sermons.Select(s => s.ToDto()).ToList());
        }
        catch (RepositoryException ex)
        {
            return Result<IReadOnlyList<SermonDto>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<SermonDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var sermon = await _repository.GetByIdAsync(id, cancellationToken);
            return sermon is null
                ? Result<SermonDto>.Failure("Sermon not found.", HttpStatusCode.NotFound)
                : Result<SermonDto>.Success(sermon.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<SermonDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<SermonDto>> CreateAsync(SermonWriteDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (validation is not null) return validation;

        try
        {
            var sermon = new Sermon();
            sermon.Apply(dto);
            await _repository.AddAsync(sermon, cancellationToken);
            return Result<SermonDto>.Success(sermon.ToDto(), statusCode: HttpStatusCode.Created);
        }
        catch (RepositoryException ex)
        {
            return Result<SermonDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<SermonDto>> UpdateAsync(Guid id, SermonWriteDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (validation is not null) return validation;

        try
        {
            var sermon = await _repository.GetByIdAsync(id, cancellationToken);
            if (sermon is null)
            {
                return Result<SermonDto>.Failure("Sermon not found.", HttpStatusCode.NotFound);
            }

            sermon.Apply(dto);
            await _repository.UpdateAsync(sermon, cancellationToken);
            return Result<SermonDto>.Success(sermon.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<SermonDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var sermon = await _repository.GetByIdAsync(id, cancellationToken);
            if (sermon is null)
            {
                return Result<bool>.Failure("Sermon not found.", HttpStatusCode.NotFound);
            }

            await _repository.DeleteAsync(sermon, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (RepositoryException ex)
        {
            return Result<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    private static Result<SermonDto>? Validate(SermonWriteDto dto)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(dto.Title)) errors.Add("Title is required.");
        if (string.IsNullOrWhiteSpace(dto.Speaker)) errors.Add("Speaker is required.");
        return errors.Count == 0 ? null : Result<SermonDto>.Failure("Sermon is invalid.", errors);
    }
}

public class EventService : IEventService
{
    private readonly IEventRepository _repository;

    public EventService(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyList<ChurchEventDto>>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var events = await _repository.GetAllAsync(publishedOnly, cancellationToken);
            return Result<IReadOnlyList<ChurchEventDto>>.Success(events.Select(e => e.ToDto()).ToList());
        }
        catch (RepositoryException ex)
        {
            return Result<IReadOnlyList<ChurchEventDto>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<IReadOnlyList<ChurchEventDto>>> GetUpcomingAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var events = await _repository.GetUpcomingAsync(publishedOnly, cancellationToken);
            return Result<IReadOnlyList<ChurchEventDto>>.Success(events.Select(e => e.ToDto()).ToList());
        }
        catch (RepositoryException ex)
        {
            return Result<IReadOnlyList<ChurchEventDto>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<ChurchEventDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        try
        {
            var churchEvent = await _repository.GetBySlugAsync(slug, cancellationToken);
            return churchEvent is null
                ? Result<ChurchEventDto>.Failure("Event not found.", HttpStatusCode.NotFound)
                : Result<ChurchEventDto>.Success(churchEvent.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<ChurchEventDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<ChurchEventDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var churchEvent = await _repository.GetByIdAsync(id, cancellationToken);
            return churchEvent is null
                ? Result<ChurchEventDto>.Failure("Event not found.", HttpStatusCode.NotFound)
                : Result<ChurchEventDto>.Success(churchEvent.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<ChurchEventDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<ChurchEventDto>> CreateAsync(ChurchEventWriteDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (validation is not null) return validation;

        try
        {
            var churchEvent = new ChurchEvent();
            churchEvent.Apply(dto, ResolveSlug(dto));
            await _repository.AddAsync(churchEvent, cancellationToken);
            return Result<ChurchEventDto>.Success(churchEvent.ToDto(), statusCode: HttpStatusCode.Created);
        }
        catch (RepositoryException ex)
        {
            return Result<ChurchEventDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<ChurchEventDto>> UpdateAsync(Guid id, ChurchEventWriteDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (validation is not null) return validation;

        try
        {
            var churchEvent = await _repository.GetByIdAsync(id, cancellationToken);
            if (churchEvent is null)
            {
                return Result<ChurchEventDto>.Failure("Event not found.", HttpStatusCode.NotFound);
            }

            churchEvent.Apply(dto, ResolveSlug(dto));
            await _repository.UpdateAsync(churchEvent, cancellationToken);
            return Result<ChurchEventDto>.Success(churchEvent.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<ChurchEventDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var churchEvent = await _repository.GetByIdAsync(id, cancellationToken);
            if (churchEvent is null)
            {
                return Result<bool>.Failure("Event not found.", HttpStatusCode.NotFound);
            }

            await _repository.DeleteAsync(churchEvent, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (RepositoryException ex)
        {
            return Result<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    private static Result<ChurchEventDto>? Validate(ChurchEventWriteDto dto)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(dto.Title)) errors.Add("Title is required.");
        return errors.Count == 0 ? null : Result<ChurchEventDto>.Failure("Event is invalid.", errors);
    }

    private static string ResolveSlug(ChurchEventWriteDto dto)
        => string.IsNullOrWhiteSpace(dto.Slug) ? SlugHelper.ToSlug(dto.Title) : SlugHelper.ToSlug(dto.Slug);
}

public class PageService : IPageService
{
    private readonly IPageRepository _repository;

    public PageService(IPageRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyList<PageDto>>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var pages = await _repository.GetAllAsync(publishedOnly, cancellationToken);
            return Result<IReadOnlyList<PageDto>>.Success(pages.Select(p => p.ToDto()).ToList());
        }
        catch (RepositoryException ex)
        {
            return Result<IReadOnlyList<PageDto>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<PageDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        try
        {
            var page = await _repository.GetBySlugAsync(slug, cancellationToken);
            return page is null
                ? Result<PageDto>.Failure("Page not found.", HttpStatusCode.NotFound)
                : Result<PageDto>.Success(page.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<PageDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<PageDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var page = await _repository.GetByIdAsync(id, cancellationToken);
            return page is null
                ? Result<PageDto>.Failure("Page not found.", HttpStatusCode.NotFound)
                : Result<PageDto>.Success(page.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<PageDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<PageDto>> CreateAsync(PageWriteDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (validation is not null) return validation;

        try
        {
            var page = new Page();
            page.Apply(dto, ResolveSlug(dto));
            await _repository.AddAsync(page, cancellationToken);
            return Result<PageDto>.Success(page.ToDto(), statusCode: HttpStatusCode.Created);
        }
        catch (RepositoryException ex)
        {
            return Result<PageDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<PageDto>> UpdateAsync(Guid id, PageWriteDto dto, CancellationToken cancellationToken = default)
    {
        var validation = Validate(dto);
        if (validation is not null) return validation;

        try
        {
            var page = await _repository.GetByIdAsync(id, cancellationToken);
            if (page is null)
            {
                return Result<PageDto>.Failure("Page not found.", HttpStatusCode.NotFound);
            }

            page.Apply(dto, ResolveSlug(dto));
            await _repository.UpdateAsync(page, cancellationToken);
            return Result<PageDto>.Success(page.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<PageDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var page = await _repository.GetByIdAsync(id, cancellationToken);
            if (page is null)
            {
                return Result<bool>.Failure("Page not found.", HttpStatusCode.NotFound);
            }

            await _repository.DeleteAsync(page, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (RepositoryException ex)
        {
            return Result<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    private static Result<PageDto>? Validate(PageWriteDto dto)
        => string.IsNullOrWhiteSpace(dto.Title)
            ? Result<PageDto>.Failure("Title is required.")
            : null;

    private static string ResolveSlug(PageWriteDto dto)
        => string.IsNullOrWhiteSpace(dto.Slug) ? SlugHelper.ToSlug(dto.Title) : SlugHelper.ToSlug(dto.Slug);
}

public class MinistryService : IMinistryService
{
    private readonly IMinistryRepository _repository;

    public MinistryService(IMinistryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyList<MinistryDto>>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var ministries = await _repository.GetAllAsync(publishedOnly, cancellationToken);
            return Result<IReadOnlyList<MinistryDto>>.Success(ministries.Select(m => m.ToDto()).ToList());
        }
        catch (RepositoryException ex)
        {
            return Result<IReadOnlyList<MinistryDto>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<MinistryDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        try
        {
            var ministry = await _repository.GetBySlugAsync(slug, cancellationToken);
            return ministry is null
                ? Result<MinistryDto>.Failure("Ministry not found.", HttpStatusCode.NotFound)
                : Result<MinistryDto>.Success(ministry.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<MinistryDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<MinistryDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var ministry = await _repository.GetByIdAsync(id, cancellationToken);
            return ministry is null
                ? Result<MinistryDto>.Failure("Ministry not found.", HttpStatusCode.NotFound)
                : Result<MinistryDto>.Success(ministry.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<MinistryDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<MinistryDto>> CreateAsync(MinistryWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return Result<MinistryDto>.Failure("Name is required.");
        }

        try
        {
            var ministry = new Ministry();
            ministry.Apply(dto, ResolveSlug(dto));
            await _repository.AddAsync(ministry, cancellationToken);
            return Result<MinistryDto>.Success(ministry.ToDto(), statusCode: HttpStatusCode.Created);
        }
        catch (RepositoryException ex)
        {
            return Result<MinistryDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<MinistryDto>> UpdateAsync(Guid id, MinistryWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return Result<MinistryDto>.Failure("Name is required.");
        }

        try
        {
            var ministry = await _repository.GetByIdAsync(id, cancellationToken);
            if (ministry is null)
            {
                return Result<MinistryDto>.Failure("Ministry not found.", HttpStatusCode.NotFound);
            }

            ministry.Apply(dto, ResolveSlug(dto));
            await _repository.UpdateAsync(ministry, cancellationToken);
            return Result<MinistryDto>.Success(ministry.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<MinistryDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var ministry = await _repository.GetByIdAsync(id, cancellationToken);
            if (ministry is null)
            {
                return Result<bool>.Failure("Ministry not found.", HttpStatusCode.NotFound);
            }

            await _repository.DeleteAsync(ministry, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (RepositoryException ex)
        {
            return Result<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    private static string ResolveSlug(MinistryWriteDto dto)
        => string.IsNullOrWhiteSpace(dto.Slug) ? SlugHelper.ToSlug(dto.Name) : SlugHelper.ToSlug(dto.Slug);
}

public class ConnectRequestService : IConnectRequestService
{
    private readonly IConnectRequestRepository _repository;

    public ConnectRequestService(IConnectRequestRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IReadOnlyList<ConnectRequestDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var requests = await _repository.GetAllAsync(cancellationToken);
            return Result<IReadOnlyList<ConnectRequestDto>>.Success(requests.Select(r => r.ToDto()).ToList());
        }
        catch (RepositoryException ex)
        {
            return Result<IReadOnlyList<ConnectRequestDto>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<ConnectRequestDto>> SubmitAsync(ConnectRequestWriteDto dto, CancellationToken cancellationToken = default)
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(dto.FirstName)) errors.Add("First name is required.");
        if (string.IsNullOrWhiteSpace(dto.LastName)) errors.Add("Last name is required.");
        if (string.IsNullOrWhiteSpace(dto.Email)) errors.Add("Email is required.");
        if (errors.Count > 0)
        {
            return Result<ConnectRequestDto>.Failure("Please complete the connect form.", errors);
        }

        try
        {
            var request = new ConnectRequest
            {
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Email = dto.Email.Trim(),
                Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : dto.Phone.Trim(),
                Message = string.IsNullOrWhiteSpace(dto.Message) ? null : dto.Message.Trim(),
                AttendsChurch = dto.AttendsChurch,
                SubmittedAtUtc = DateTime.UtcNow
            };

            await _repository.AddAsync(request, cancellationToken);
            return Result<ConnectRequestDto>.Success(request.ToDto(), "Thank you. We will be in touch.", statusCode: HttpStatusCode.Created);
        }
        catch (RepositoryException ex)
        {
            return Result<ConnectRequestDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<ConnectRequestDto>> MarkReadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var request = await _repository.GetByIdAsync(id, cancellationToken);
            if (request is null)
            {
                return Result<ConnectRequestDto>.Failure("Connect request not found.", HttpStatusCode.NotFound);
            }

            request.IsRead = true;
            await _repository.UpdateAsync(request, cancellationToken);
            return Result<ConnectRequestDto>.Success(request.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<ConnectRequestDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}

public class ChurchSettingsService : IChurchSettingsService
{
    private readonly IChurchSettingsRepository _repository;

    public ChurchSettingsService(IChurchSettingsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ChurchSettingsDto>> GetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var settings = await _repository.GetAsync(cancellationToken);
            return Result<ChurchSettingsDto>.Success(settings.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<ChurchSettingsDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<ChurchSettingsDto>> UpdateAsync(ChurchSettingsWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return Result<ChurchSettingsDto>.Failure("Church name is required.");
        }

        try
        {
            var settings = await _repository.GetAsync(cancellationToken);
            settings.Apply(dto);
            await _repository.UpdateAsync(settings, cancellationToken);
            return Result<ChurchSettingsDto>.Success(settings.ToDto());
        }
        catch (RepositoryException ex)
        {
            return Result<ChurchSettingsDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}
