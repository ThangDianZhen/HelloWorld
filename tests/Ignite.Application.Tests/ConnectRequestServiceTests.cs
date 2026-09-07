using Ignite.Application.Dto.Application;
using Ignite.Application.Interfaces.Persistence.Repositories;
using Ignite.Application.Services.Entities;
using Ignite.Core.Entities;

namespace Ignite.Application.Tests;

public class ConnectRequestServiceTests
{
    [Fact]
    public async Task SubmitAsync_RequiresEmail()
    {
        var service = new ConnectRequestService(new FakeConnectRepository());
        var result = await service.SubmitAsync(new ConnectRequestWriteDto
        {
            FirstName = "Ava",
            LastName = "Tan"
        });

        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Contains("Email"));
    }

    [Fact]
    public async Task SubmitAsync_StoresRequest()
    {
        var repo = new FakeConnectRepository();
        var service = new ConnectRequestService(repo);
        var result = await service.SubmitAsync(new ConnectRequestWriteDto
        {
            FirstName = "Ava",
            LastName = "Tan",
            Email = "ava@example.com",
            Message = "I would like to visit this Sunday."
        });

        Assert.True(result.IsSuccess);
        Assert.Equal("Ava", result.Data!.FirstName);
        Assert.False(result.Data.IsRead);
        Assert.Single(repo.Items);
    }

    [Fact]
    public async Task MarkReadAsync_UpdatesFlag()
    {
        var repo = new FakeConnectRepository();
        var existing = new ConnectRequest { FirstName = "Ava", LastName = "Tan", Email = "ava@example.com" };
        repo.Items.Add(existing);
        var service = new ConnectRequestService(repo);

        var result = await service.MarkReadAsync(existing.Id);

        Assert.True(result.IsSuccess);
        Assert.True(result.Data!.IsRead);
    }

    private sealed class FakeConnectRepository : IConnectRequestRepository
    {
        public List<ConnectRequest> Items { get; } = [];

        public Task AddAsync(ConnectRequest request, CancellationToken cancellationToken = default)
        {
            Items.Add(request);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<ConnectRequest>> GetAllAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<ConnectRequest>>(Items);

        public Task<ConnectRequest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => Task.FromResult(Items.FirstOrDefault(i => i.Id == id));

        public Task UpdateAsync(ConnectRequest request, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
