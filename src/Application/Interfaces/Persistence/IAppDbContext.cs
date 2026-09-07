using Ignite.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ignite.Application.Interfaces.Persistence;

public interface IAppDbContext
{
    DbSet<Campus> Campuses { get; }
    DbSet<ServiceTime> ServiceTimes { get; }
    DbSet<Sermon> Sermons { get; }
    DbSet<ChurchEvent> Events { get; }
    DbSet<Page> Pages { get; }
    DbSet<Ministry> Ministries { get; }
    DbSet<ConnectRequest> ConnectRequests { get; }
    DbSet<ChurchSettings> ChurchSettings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
