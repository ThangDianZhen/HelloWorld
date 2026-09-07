using Ignite.Application.Dto.Application;
using Ignite.Application.Results;

namespace Ignite.Application.Interfaces.Services.Application;

public interface IChurchSiteService
{
    Task<Result<HomePageDto>> GetHomeAsync(CancellationToken cancellationToken = default);
}
