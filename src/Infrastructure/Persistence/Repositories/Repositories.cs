using Ignite.Application.Exceptions;
using Ignite.Application.Interfaces.Persistence.Repositories;
using Ignite.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ignite.Infrastructure.Persistence.Repositories;

public class CampusRepository : ICampusRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<CampusRepository> _logger;

    public CampusRepository(AppDbContext db, ILogger<CampusRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Campus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _db.Campuses
                .Include(c => c.ServiceTimes)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load campus {CampusId}", id);
            throw new RepositoryException("Unable to load campus.", ex);
        }
    }

    public async Task<Campus?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _db.Campuses
                .Include(c => c.ServiceTimes)
                .FirstOrDefaultAsync(c => c.Slug == slug, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load campus slug {Slug}", slug);
            throw new RepositoryException("Unable to load campus.", ex);
        }
    }

    public async Task<IReadOnlyList<Campus>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _db.Campuses.Include(c => c.ServiceTimes).AsQueryable();
            if (publishedOnly)
            {
                query = query.Where(c => c.IsPublished);
            }

            return await query
                .OrderBy(c => c.SortOrder)
                .ThenBy(c => c.Name)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list campuses");
            throw new RepositoryException("Unable to list campuses.", ex);
        }
    }

    public async Task AddAsync(Campus campus, CancellationToken cancellationToken = default)
    {
        try
        {
            _db.Campuses.Add(campus);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create campus");
            throw new RepositoryException("Unable to create campus.", ex);
        }
    }

    public async Task UpdateAsync(Campus campus, CancellationToken cancellationToken = default)
    {
        try
        {
            _db.Campuses.Update(campus);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update campus {CampusId}", campus.Id);
            throw new RepositoryException("Unable to update campus.", ex);
        }
    }

    public async Task DeleteAsync(Campus campus, CancellationToken cancellationToken = default)
    {
        try
        {
            _db.Campuses.Remove(campus);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete campus {CampusId}", campus.Id);
            throw new RepositoryException("Unable to delete campus.", ex);
        }
    }
}

public class SermonRepository : ISermonRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<SermonRepository> _logger;

    public SermonRepository(AppDbContext db, ILogger<SermonRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Sermon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _db.Sermons.Include(s => s.Campus).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load sermon {SermonId}", id);
            throw new RepositoryException("Unable to load sermon.", ex);
        }
    }

    public async Task<IReadOnlyList<Sermon>> GetLatestAsync(int take, bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _db.Sermons.Include(s => s.Campus).AsQueryable();
            if (publishedOnly)
            {
                query = query.Where(s => s.IsPublished);
            }

            return await query
                .OrderByDescending(s => s.PreachedOn)
                .Take(take)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list sermons");
            throw new RepositoryException("Unable to list sermons.", ex);
        }
    }

    public async Task AddAsync(Sermon sermon, CancellationToken cancellationToken = default)
        => await Save(() => _db.Sermons.Add(sermon), "Unable to create sermon.", cancellationToken);

    public async Task UpdateAsync(Sermon sermon, CancellationToken cancellationToken = default)
        => await Save(() => _db.Sermons.Update(sermon), "Unable to update sermon.", cancellationToken);

    public async Task DeleteAsync(Sermon sermon, CancellationToken cancellationToken = default)
        => await Save(() => _db.Sermons.Remove(sermon), "Unable to delete sermon.", cancellationToken);

    private async Task Save(Action mutate, string error, CancellationToken cancellationToken)
    {
        try
        {
            mutate();
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Error}", error);
            throw new RepositoryException(error, ex);
        }
    }
}

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<EventRepository> _logger;

    public EventRepository(AppDbContext db, ILogger<EventRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ChurchEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _db.Events.Include(e => e.Campus).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load event {EventId}", id);
            throw new RepositoryException("Unable to load event.", ex);
        }
    }

    public async Task<ChurchEvent?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _db.Events.Include(e => e.Campus).FirstOrDefaultAsync(e => e.Slug == slug, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load event slug {Slug}", slug);
            throw new RepositoryException("Unable to load event.", ex);
        }
    }

    public async Task<IReadOnlyList<ChurchEvent>> GetUpcomingAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var now = DateTime.UtcNow;
            var query = _db.Events.Include(e => e.Campus).Where(e => e.StartsAtUtc >= now);
            if (publishedOnly)
            {
                query = query.Where(e => e.IsPublished);
            }

            return await query.OrderBy(e => e.StartsAtUtc).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list upcoming events");
            throw new RepositoryException("Unable to list events.", ex);
        }
    }

    public async Task<IReadOnlyList<ChurchEvent>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _db.Events.Include(e => e.Campus).AsQueryable();
            if (publishedOnly)
            {
                query = query.Where(e => e.IsPublished);
            }

            return await query.OrderBy(e => e.StartsAtUtc).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list events");
            throw new RepositoryException("Unable to list events.", ex);
        }
    }

    public async Task AddAsync(ChurchEvent churchEvent, CancellationToken cancellationToken = default)
        => await Save(() => _db.Events.Add(churchEvent), "Unable to create event.", cancellationToken);

    public async Task UpdateAsync(ChurchEvent churchEvent, CancellationToken cancellationToken = default)
        => await Save(() => _db.Events.Update(churchEvent), "Unable to update event.", cancellationToken);

    public async Task DeleteAsync(ChurchEvent churchEvent, CancellationToken cancellationToken = default)
        => await Save(() => _db.Events.Remove(churchEvent), "Unable to delete event.", cancellationToken);

    private async Task Save(Action mutate, string error, CancellationToken cancellationToken)
    {
        try
        {
            mutate();
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Error}", error);
            throw new RepositoryException(error, ex);
        }
    }
}

public class PageRepository : IPageRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<PageRepository> _logger;

    public PageRepository(AppDbContext db, ILogger<PageRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Page?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Safe(() => _db.Pages.FirstOrDefaultAsync(p => p.Id == id, cancellationToken), "Unable to load page.");

    public async Task<Page?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        => await Safe(() => _db.Pages.FirstOrDefaultAsync(p => p.Slug == slug, cancellationToken), "Unable to load page.");

    public async Task<IReadOnlyList<Page>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _db.Pages.AsQueryable();
            if (publishedOnly)
            {
                query = query.Where(p => p.IsPublished);
            }

            return await query.OrderBy(p => p.SortOrder).ThenBy(p => p.Title).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list pages");
            throw new RepositoryException("Unable to list pages.", ex);
        }
    }

    public async Task AddAsync(Page page, CancellationToken cancellationToken = default)
        => await Save(() => _db.Pages.Add(page), "Unable to create page.", cancellationToken);

    public async Task UpdateAsync(Page page, CancellationToken cancellationToken = default)
        => await Save(() => _db.Pages.Update(page), "Unable to update page.", cancellationToken);

    public async Task DeleteAsync(Page page, CancellationToken cancellationToken = default)
        => await Save(() => _db.Pages.Remove(page), "Unable to delete page.", cancellationToken);

    private async Task<T> Safe<T>(Func<Task<T>> action, string error)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Error}", error);
            throw new RepositoryException(error, ex);
        }
    }

    private async Task Save(Action mutate, string error, CancellationToken cancellationToken)
    {
        try
        {
            mutate();
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Error}", error);
            throw new RepositoryException(error, ex);
        }
    }
}

public class MinistryRepository : IMinistryRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<MinistryRepository> _logger;

    public MinistryRepository(AppDbContext db, ILogger<MinistryRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Ministry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await Safe(() => _db.Ministries.FirstOrDefaultAsync(m => m.Id == id, cancellationToken), "Unable to load ministry.");

    public async Task<Ministry?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        => await Safe(() => _db.Ministries.FirstOrDefaultAsync(m => m.Slug == slug, cancellationToken), "Unable to load ministry.");

    public async Task<IReadOnlyList<Ministry>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = _db.Ministries.AsQueryable();
            if (publishedOnly)
            {
                query = query.Where(m => m.IsPublished);
            }

            return await query.OrderBy(m => m.SortOrder).ThenBy(m => m.Name).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list ministries");
            throw new RepositoryException("Unable to list ministries.", ex);
        }
    }

    public async Task AddAsync(Ministry ministry, CancellationToken cancellationToken = default)
        => await Save(() => _db.Ministries.Add(ministry), "Unable to create ministry.", cancellationToken);

    public async Task UpdateAsync(Ministry ministry, CancellationToken cancellationToken = default)
        => await Save(() => _db.Ministries.Update(ministry), "Unable to update ministry.", cancellationToken);

    public async Task DeleteAsync(Ministry ministry, CancellationToken cancellationToken = default)
        => await Save(() => _db.Ministries.Remove(ministry), "Unable to delete ministry.", cancellationToken);

    private async Task<T> Safe<T>(Func<Task<T>> action, string error)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Error}", error);
            throw new RepositoryException(error, ex);
        }
    }

    private async Task Save(Action mutate, string error, CancellationToken cancellationToken)
    {
        try
        {
            mutate();
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Error}", error);
            throw new RepositoryException(error, ex);
        }
    }
}

public class ConnectRequestRepository : IConnectRequestRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<ConnectRequestRepository> _logger;

    public ConnectRequestRepository(AppDbContext db, ILogger<ConnectRequestRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ConnectRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _db.ConnectRequests.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load connect request {Id}", id);
            throw new RepositoryException("Unable to load connect request.", ex);
        }
    }

    public async Task<IReadOnlyList<ConnectRequest>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _db.ConnectRequests
                .OrderByDescending(r => r.SubmittedAtUtc)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list connect requests");
            throw new RepositoryException("Unable to list connect requests.", ex);
        }
    }

    public async Task AddAsync(ConnectRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            _db.ConnectRequests.Add(request);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save connect request");
            throw new RepositoryException("Unable to save connect request.", ex);
        }
    }

    public async Task UpdateAsync(ConnectRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            _db.ConnectRequests.Update(request);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update connect request {Id}", request.Id);
            throw new RepositoryException("Unable to update connect request.", ex);
        }
    }
}

public class ChurchSettingsRepository : IChurchSettingsRepository
{
    private readonly AppDbContext _db;
    private readonly ILogger<ChurchSettingsRepository> _logger;

    public ChurchSettingsRepository(AppDbContext db, ILogger<ChurchSettingsRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ChurchSettings> GetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var settings = await _db.ChurchSettings.FirstOrDefaultAsync(cancellationToken);
            if (settings is null)
            {
                throw new RepositoryException("Church settings have not been created yet.");
            }

            return settings;
        }
        catch (RepositoryException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load church settings");
            throw new RepositoryException("Unable to load church settings.", ex);
        }
    }

    public async Task UpdateAsync(ChurchSettings settings, CancellationToken cancellationToken = default)
    {
        try
        {
            _db.ChurchSettings.Update(settings);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update church settings");
            throw new RepositoryException("Unable to update church settings.", ex);
        }
    }
}
