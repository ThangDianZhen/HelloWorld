using Application.Dto.Entities;
using Application.Results;

namespace Application.Interfaces.Services.Entities;

public interface IGreetingService
{
    Task<Result<GreetingDto>> CreateGreetingAsync(string name, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<GreetingDto>>> GetAllAsync(CancellationToken cancellationToken = default);
}
