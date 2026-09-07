using Ignite.Application.Dto.Application;
using Ignite.Application.Interfaces.Persistence.Repositories;
using Ignite.Application.Services.Entities;
using Ignite.Core.Entities;

namespace Ignite.Application.Tests;

public class CampusServiceTests
{
    [Fact]
    public async Task CreateAsync_RejectsMissingName()
    {
        var service = new CampusService(new FakeCampusRepository());
        var result = await service.CreateAsync(new CampusWriteDto { City = "KL", Country = "Malaysia" });
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, e => e.Contains("Name"));
    }

    [Fact]
    public async Task CreateAsync_SavesCampusAndServiceTimes()
    {
        var repo = new FakeCampusRepository();
        var service = new CampusService(repo);
        var result = await service.CreateAsync(new CampusWriteDto
        {
            Name = "Ignite Penang",
            City = "George Town",
            Country = "Malaysia",
            ServiceTimes =
            [
                new ServiceTimeWriteDto { TimeLabel = "10:00 AM", Title = "Main Service" }
            ]
        });

        Assert.True(result.IsSuccess);
        Assert.Equal("ignite-penang", result.Data!.Slug);
        Assert.Single(result.Data.ServiceTimes);
        Assert.Single(repo.Items);
    }

    [Fact]
    public async Task GetBySlugAsync_ReturnsNotFound()
    {
        var service = new CampusService(new FakeCampusRepository());
        var result = await service.GetBySlugAsync("missing");
        Assert.False(result.IsSuccess);
        Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
    }

    private sealed class FakeCampusRepository : ICampusRepository
    {
        public List<Campus> Items { get; } = [];

        public Task AddAsync(Campus campus, CancellationToken cancellationToken = default)
        {
            Items.Add(campus);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Campus campus, CancellationToken cancellationToken = default)
        {
            Items.Remove(campus);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Campus>> GetAllAsync(bool publishedOnly, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Campus>>(publishedOnly ? Items.Where(i => i.IsPublished).ToList() : Items);

        public Task<Campus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => Task.FromResult(Items.FirstOrDefault(i => i.Id == id));

        public Task<Campus?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
            => Task.FromResult(Items.FirstOrDefault(i => i.Slug == slug));

        public Task UpdateAsync(Campus campus, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
