using Core.Entities;

namespace Application.Interfaces.Persistence.Repositories;

public interface IGreetingRepository
{
    Task<Greeting> AddAsync(Greeting greeting, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Greeting>> GetAllAsync(CancellationToken cancellationToken = default);
}
