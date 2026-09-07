using Ignite.Application.Dto.Entities;
using Ignite.Core.Enums;

namespace Ignite.Application.Dto.Application;

public class HomePageDto
{
    public ChurchSettingsDto Settings { get; set; } = default!;
    public List<CampusDto> Campuses { get; set; } = [];
    public List<SermonDto> LatestSermons { get; set; } = [];
    public List<ChurchEventDto> UpcomingEvents { get; set; } = [];
    public List<MinistryDto> Ministries { get; set; } = [];
}

public class ServiceTimeWriteDto
{
    public Guid? Id { get; set; }
    public ServiceDay Day { get; set; } = ServiceDay.Sunday;
    public string TimeLabel { get; set; } = string.Empty;
    public string Title { get; set; } = "Main Service";
    public string? Notes { get; set; }
    public int SortOrder { get; set; }
}

public class CampusWriteDto
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string City { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? MapUrl { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Description { get; set; }
    public bool IsOnlineCampus { get; set; }
    public bool IsPublished { get; set; } = true;
    public int SortOrder { get; set; }
    public List<ServiceTimeWriteDto> ServiceTimes { get; set; } = [];
}

public class SermonWriteDto
{
    public string Title { get; set; } = string.Empty;
    public string Speaker { get; set; } = string.Empty;
    public string? Series { get; set; }
    public DateTime PreachedOn { get; set; } = DateTime.UtcNow.Date;
    public string? VideoUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public Guid? CampusId { get; set; }
    public bool IsPublished { get; set; } = true;
}

public class ChurchEventWriteDto
{
    public string Title { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime StartsAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? EndsAtUtc { get; set; }
    public string? LocationName { get; set; }
    public Guid? CampusId { get; set; }
    public string? ImageUrl { get; set; }
    public string? RegistrationUrl { get; set; }
    public bool IsPublished { get; set; } = true;
}

public class PageWriteDto
{
    public string Title { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string BodyHtml { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public bool IsPublished { get; set; } = true;
    public int SortOrder { get; set; }
}

public class MinistryWriteDto
{
    public string Name { get; set; } = string.Empty;
    public string? Slug { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? LeaderName { get; set; }
    public bool IsPublished { get; set; } = true;
    public int SortOrder { get; set; }
}

public class ConnectRequestWriteDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Message { get; set; }
    public bool AttendsChurch { get; set; }
}

public class ChurchSettingsWriteDto
{
    public string Name { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string AboutHtml { get; set; } = string.Empty;
    public string HeroTitle { get; set; } = string.Empty;
    public string HeroSubtitle { get; set; } = string.Empty;
    public string? HeroVideoUrl { get; set; }
    public string? LivestreamUrl { get; set; }
    public string? GivingUrl { get; set; }
    public string GivingHeadline { get; set; } = "Give online";
    public string GivingBody { get; set; } = string.Empty;
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? LogoUrl { get; set; }
    public string FooterNote { get; set; } = string.Empty;
}
