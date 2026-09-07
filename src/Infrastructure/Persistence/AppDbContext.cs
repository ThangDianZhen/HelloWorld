using Ignite.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ignite.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<IdentityUser>, Application.Interfaces.Persistence.IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Campus> Campuses => Set<Campus>();
    public DbSet<ServiceTime> ServiceTimes => Set<ServiceTime>();
    public DbSet<Sermon> Sermons => Set<Sermon>();
    public DbSet<ChurchEvent> Events => Set<ChurchEvent>();
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<Ministry> Ministries => Set<Ministry>();
    public DbSet<ConnectRequest> ConnectRequests => Set<ConnectRequest>();
    public DbSet<ChurchSettings> ChurchSettings => Set<ChurchSettings>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
