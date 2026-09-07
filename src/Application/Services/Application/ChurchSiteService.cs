using System.Net;
using Ignite.Application.Dto.Application;
using Ignite.Application.Dto.Extensions;
using Ignite.Application.Exceptions;
using Ignite.Application.Interfaces.Persistence.Repositories;
using Ignite.Application.Interfaces.Services.Application;
using Ignite.Application.Results;

namespace Ignite.Application.Services.Application;

public class ChurchSiteService : IChurchSiteService
{
    private readonly IChurchSettingsRepository _settingsRepository;
    private readonly ICampusRepository _campusRepository;
    private readonly ISermonRepository _sermonRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IMinistryRepository _ministryRepository;

    public ChurchSiteService(
        IChurchSettingsRepository settingsRepository,
        ICampusRepository campusRepository,
        ISermonRepository sermonRepository,
        IEventRepository eventRepository,
        IMinistryRepository ministryRepository)
    {
        _settingsRepository = settingsRepository;
        _campusRepository = campusRepository;
        _sermonRepository = sermonRepository;
        _eventRepository = eventRepository;
        _ministryRepository = ministryRepository;
    }

    public async Task<Result<HomePageDto>> GetHomeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var settings = await _settingsRepository.GetAsync(cancellationToken);
            var campuses = await _campusRepository.GetAllAsync(publishedOnly: true, cancellationToken);
            var sermons = await _sermonRepository.GetLatestAsync(3, publishedOnly: true, cancellationToken);
            var events = await _eventRepository.GetUpcomingAsync(publishedOnly: true, cancellationToken);
            var ministries = await _ministryRepository.GetAllAsync(publishedOnly: true, cancellationToken);

            return Result<HomePageDto>.Success(new HomePageDto
            {
                Settings = settings.ToDto(),
                Campuses = campuses.Select(c => c.ToDto()).ToList(),
                LatestSermons = sermons.Select(s => s.ToDto()).ToList(),
                UpcomingEvents = events.Take(3).Select(e => e.ToDto()).ToList(),
                Ministries = ministries.Select(m => m.ToDto()).ToList()
            });
        }
        catch (RepositoryException ex)
        {
            return Result<HomePageDto>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}
