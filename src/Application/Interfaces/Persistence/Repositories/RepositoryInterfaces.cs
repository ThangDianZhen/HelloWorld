using Ignite.Core.Entities;

namespace Ignite.Application.Interfaces.Persistence.Repositories;

public interface ICampusRepository
{
    Task<Campus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Campus?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Campus>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task AddAsync(Campus campus, CancellationToken cancellationToken = default);
    Task UpdateAsync(Campus campus, CancellationToken cancellationToken = default);
    Task DeleteAsync(Campus campus, CancellationToken cancellationToken = default);
}

public interface ISermonRepository
{
    Task<Sermon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Sermon>> GetLatestAsync(int take, bool publishedOnly, CancellationToken cancellationToken = default);
    Task AddAsync(Sermon sermon, CancellationToken cancellationToken = default);
    Task UpdateAsync(Sermon sermon, CancellationToken cancellationToken = default);
    Task DeleteAsync(Sermon sermon, CancellationToken cancellationToken = default);
}

public interface IEventRepository
{
    Task<ChurchEvent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ChurchEvent?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChurchEvent>> GetUpcomingAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ChurchEvent>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task AddAsync(ChurchEvent churchEvent, CancellationToken cancellationToken = default);
    Task UpdateAsync(ChurchEvent churchEvent, CancellationToken cancellationToken = default);
    Task DeleteAsync(ChurchEvent churchEvent, CancellationToken cancellationToken = default);
}

public interface IPageRepository
{
    Task<Page?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Page?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Page>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task AddAsync(Page page, CancellationToken cancellationToken = default);
    Task UpdateAsync(Page page, CancellationToken cancellationToken = default);
    Task DeleteAsync(Page page, CancellationToken cancellationToken = default);
}

public interface IMinistryRepository
{
    Task<Ministry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Ministry?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Ministry>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task AddAsync(Ministry ministry, CancellationToken cancellationToken = default);
    Task UpdateAsync(Ministry ministry, CancellationToken cancellationToken = default);
    Task DeleteAsync(Ministry ministry, CancellationToken cancellationToken = default);
}

public interface IConnectRequestRepository
{
    Task<ConnectRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ConnectRequest>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(ConnectRequest request, CancellationToken cancellationToken = default);
    Task UpdateAsync(ConnectRequest request, CancellationToken cancellationToken = default);
}

public interface IChurchSettingsRepository
{
    Task<ChurchSettings> GetAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(ChurchSettings settings, CancellationToken cancellationToken = default);
}
