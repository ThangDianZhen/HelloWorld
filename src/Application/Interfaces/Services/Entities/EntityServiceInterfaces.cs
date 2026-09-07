using Ignite.Application.Dto.Application;
using Ignite.Application.Dto.Entities;
using Ignite.Application.Results;

namespace Ignite.Application.Interfaces.Services.Entities;

public interface ICampusService
{
    Task<Result<IReadOnlyList<CampusDto>>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task<Result<CampusDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<CampusDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Result<CampusDto>> CreateAsync(CampusWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<CampusDto>> UpdateAsync(Guid id, CampusWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface ISermonService
{
    Task<Result<IReadOnlyList<SermonDto>>> GetLatestAsync(int take, bool publishedOnly, CancellationToken cancellationToken = default);
    Task<Result<SermonDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<SermonDto>> CreateAsync(SermonWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<SermonDto>> UpdateAsync(Guid id, SermonWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IEventService
{
    Task<Result<IReadOnlyList<ChurchEventDto>>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<ChurchEventDto>>> GetUpcomingAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task<Result<ChurchEventDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Result<ChurchEventDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<ChurchEventDto>> CreateAsync(ChurchEventWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<ChurchEventDto>> UpdateAsync(Guid id, ChurchEventWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IPageService
{
    Task<Result<IReadOnlyList<PageDto>>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task<Result<PageDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Result<PageDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<PageDto>> CreateAsync(PageWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<PageDto>> UpdateAsync(Guid id, PageWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IMinistryService
{
    Task<Result<IReadOnlyList<MinistryDto>>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default);
    Task<Result<MinistryDto>> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);
    Task<Result<MinistryDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<MinistryDto>> CreateAsync(MinistryWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<MinistryDto>> UpdateAsync(Guid id, MinistryWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IConnectRequestService
{
    Task<Result<IReadOnlyList<ConnectRequestDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ConnectRequestDto>> SubmitAsync(ConnectRequestWriteDto dto, CancellationToken cancellationToken = default);
    Task<Result<ConnectRequestDto>> MarkReadAsync(Guid id, CancellationToken cancellationToken = default);
}

public interface IChurchSettingsService
{
    Task<Result<ChurchSettingsDto>> GetAsync(CancellationToken cancellationToken = default);
    Task<Result<ChurchSettingsDto>> UpdateAsync(ChurchSettingsWriteDto dto, CancellationToken cancellationToken = default);
}
