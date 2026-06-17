using System.Collections.Concurrent;
using Application.Interfaces.Persistence.Repositories;
using Core.Entities;

namespace Infrastructure.Persistence.Repositories;

public class GreetingRepository : IGreetingRepository
{
    private readonly ConcurrentDictionary<Guid, Greeting> _store = new();

    public Task<Greeting> AddAsync(Greeting greeting, CancellationToken cancellationToken = default)
    {
        _store[greeting.Id] = greeting;
        return Task.FromResult(greeting);
    }

    public Task<IReadOnlyList<Greeting>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Greeting> greetings = _store.Values
            .OrderBy(g => g.CreatedAtUtc)
            .ToList();
        return Task.FromResult(greetings);
    }
}
