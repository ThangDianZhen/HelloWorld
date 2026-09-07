using Ignite.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ignite.Infrastructure.Persistence.Configurations;

public class CampusConfiguration : IEntityTypeConfiguration<Campus>
{
    public void Configure(EntityTypeBuilder<Campus> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(120);
        builder.Property(c => c.Slug).IsRequired().HasMaxLength(140);
        builder.HasIndex(c => c.Slug).IsUnique();
        builder.Property(c => c.City).IsRequired().HasMaxLength(80);
        builder.Property(c => c.Region).HasMaxLength(80);
        builder.Property(c => c.Country).IsRequired().HasMaxLength(80);
        builder.Property(c => c.Address).HasMaxLength(250);
        builder.Property(c => c.ContactEmail).HasMaxLength(120);
        builder.Property(c => c.ContactPhone).HasMaxLength(40);
        builder.HasMany(c => c.ServiceTimes)
            .WithOne(t => t.Campus)
            .HasForeignKey(t => t.CampusId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ServiceTimeConfiguration : IEntityTypeConfiguration<ServiceTime>
{
    public void Configure(EntityTypeBuilder<ServiceTime> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TimeLabel).IsRequired().HasMaxLength(40);
        builder.Property(t => t.Title).IsRequired().HasMaxLength(80);
        builder.Property(t => t.Notes).HasMaxLength(250);
    }
}

public class SermonConfiguration : IEntityTypeConfiguration<Sermon>
{
    public void Configure(EntityTypeBuilder<Sermon> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Title).IsRequired().HasMaxLength(180);
        builder.Property(s => s.Speaker).IsRequired().HasMaxLength(120);
        builder.Property(s => s.Series).HasMaxLength(120);
        builder.HasOne(s => s.Campus)
            .WithMany()
            .HasForeignKey(s => s.CampusId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class ChurchEventConfiguration : IEntityTypeConfiguration<ChurchEvent>
{
    public void Configure(EntityTypeBuilder<ChurchEvent> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(180);
        builder.Property(e => e.Slug).IsRequired().HasMaxLength(180);
        builder.HasIndex(e => e.Slug).IsUnique();
        builder.Property(e => e.LocationName).HasMaxLength(180);
        builder.HasOne(e => e.Campus)
            .WithMany()
            .HasForeignKey(e => e.CampusId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Title).IsRequired().HasMaxLength(180);
        builder.Property(p => p.Slug).IsRequired().HasMaxLength(180);
        builder.HasIndex(p => p.Slug).IsUnique();
        builder.Property(p => p.Summary).HasMaxLength(300);
        builder.Property(p => p.BodyHtml).IsRequired();
    }
}

public class MinistryConfiguration : IEntityTypeConfiguration<Ministry>
{
    public void Configure(EntityTypeBuilder<Ministry> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).IsRequired().HasMaxLength(120);
        builder.Property(m => m.Slug).IsRequired().HasMaxLength(140);
        builder.HasIndex(m => m.Slug).IsUnique();
        builder.Property(m => m.LeaderName).HasMaxLength(120);
    }
}

public class ConnectRequestConfiguration : IEntityTypeConfiguration<ConnectRequest>
{
    public void Configure(EntityTypeBuilder<ConnectRequest> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.FirstName).IsRequired().HasMaxLength(80);
        builder.Property(r => r.LastName).IsRequired().HasMaxLength(80);
        builder.Property(r => r.Email).IsRequired().HasMaxLength(120);
        builder.Property(r => r.Phone).HasMaxLength(40);
    }
}

public class ChurchSettingsConfiguration : IEntityTypeConfiguration<ChurchSettings>
{
    public void Configure(EntityTypeBuilder<ChurchSettings> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(120);
        builder.Property(s => s.Tagline).HasMaxLength(250);
        builder.Property(s => s.HeroTitle).HasMaxLength(180);
        builder.Property(s => s.HeroSubtitle).HasMaxLength(400);
        builder.Property(s => s.GivingHeadline).HasMaxLength(180);
        builder.Property(s => s.ContactEmail).HasMaxLength(120);
        builder.Property(s => s.ContactPhone).HasMaxLength(40);
        builder.Property(s => s.FooterNote).HasMaxLength(250);
    }
}
