using Ignite.Core.Entities;
using Ignite.Core.Enums;

namespace Ignite.Application.Dto.Entities;

public class ServiceTimeDto
{
    public Guid Id { get; private set; }
    public ServiceDay Day { get; private set; }
    public string TimeLabel { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string? Notes { get; private set; }
    public int SortOrder { get; private set; }

    public ServiceTimeDto(Guid id, ServiceDay day, string timeLabel, string title, string? notes, int sortOrder)
    {
        Id = id;
        Day = day;
        TimeLabel = timeLabel;
        Title = title;
        Notes = notes;
        SortOrder = sortOrder;
    }
}

public class CampusDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string Region { get; private set; } = default!;
    public string Country { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string? ImageUrl { get; private set; }
    public string? MapUrl { get; private set; }
    public string? ContactEmail { get; private set; }
    public string? ContactPhone { get; private set; }
    public string? Description { get; private set; }
    public bool IsOnlineCampus { get; private set; }
    public bool IsPublished { get; private set; }
    public int SortOrder { get; private set; }
    public List<ServiceTimeDto> ServiceTimes { get; set; } = [];

    public CampusDto(
        Guid id,
        string name,
        string slug,
        string city,
        string region,
        string country,
        string address,
        string? imageUrl,
        string? mapUrl,
        string? contactEmail,
        string? contactPhone,
        string? description,
        bool isOnlineCampus,
        bool isPublished,
        int sortOrder)
    {
        Id = id;
        Name = name;
        Slug = slug;
        City = city;
        Region = region;
        Country = country;
        Address = address;
        ImageUrl = imageUrl;
        MapUrl = mapUrl;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
        Description = description;
        IsOnlineCampus = isOnlineCampus;
        IsPublished = isPublished;
        SortOrder = sortOrder;
    }
}

public class SermonDto
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = default!;
    public string Speaker { get; private set; } = default!;
    public string? Series { get; private set; }
    public DateTime PreachedOn { get; private set; }
    public string? VideoUrl { get; private set; }
    public string? AudioUrl { get; private set; }
    public string? Description { get; private set; }
    public string? ThumbnailUrl { get; private set; }
    public Guid? CampusId { get; private set; }
    public string? CampusName { get; private set; }
    public bool IsPublished { get; private set; }

    public SermonDto(
        Guid id,
        string title,
        string speaker,
        string? series,
        DateTime preachedOn,
        string? videoUrl,
        string? audioUrl,
        string? description,
        string? thumbnailUrl,
        Guid? campusId,
        string? campusName,
        bool isPublished)
    {
        Id = id;
        Title = title;
        Speaker = speaker;
        Series = series;
        PreachedOn = preachedOn;
        VideoUrl = videoUrl;
        AudioUrl = audioUrl;
        Description = description;
        ThumbnailUrl = thumbnailUrl;
        CampusId = campusId;
        CampusName = campusName;
        IsPublished = isPublished;
    }
}

public class ChurchEventDto
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public DateTime StartsAtUtc { get; private set; }
    public DateTime? EndsAtUtc { get; private set; }
    public string? LocationName { get; private set; }
    public Guid? CampusId { get; private set; }
    public string? CampusName { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? RegistrationUrl { get; private set; }
    public bool IsPublished { get; private set; }

    public ChurchEventDto(
        Guid id,
        string title,
        string slug,
        string description,
        DateTime startsAtUtc,
        DateTime? endsAtUtc,
        string? locationName,
        Guid? campusId,
        string? campusName,
        string? imageUrl,
        string? registrationUrl,
        bool isPublished)
    {
        Id = id;
        Title = title;
        Slug = slug;
        Description = description;
        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;
        LocationName = locationName;
        CampusId = campusId;
        CampusName = campusName;
        ImageUrl = imageUrl;
        RegistrationUrl = registrationUrl;
        IsPublished = isPublished;
    }
}

public class PageDto
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string BodyHtml { get; private set; } = default!;
    public string? Summary { get; private set; }
    public bool IsPublished { get; private set; }
    public int SortOrder { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    public PageDto(
        Guid id,
        string title,
        string slug,
        string bodyHtml,
        string? summary,
        bool isPublished,
        int sortOrder,
        DateTime updatedAtUtc)
    {
        Id = id;
        Title = title;
        Slug = slug;
        BodyHtml = bodyHtml;
        Summary = summary;
        IsPublished = isPublished;
        SortOrder = sortOrder;
        UpdatedAtUtc = updatedAtUtc;
    }
}

public class MinistryDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string? ImageUrl { get; private set; }
    public string? LeaderName { get; private set; }
    public bool IsPublished { get; private set; }
    public int SortOrder { get; private set; }

    public MinistryDto(
        Guid id,
        string name,
        string slug,
        string description,
        string? imageUrl,
        string? leaderName,
        bool isPublished,
        int sortOrder)
    {
        Id = id;
        Name = name;
        Slug = slug;
        Description = description;
        ImageUrl = imageUrl;
        LeaderName = leaderName;
        IsPublished = isPublished;
        SortOrder = sortOrder;
    }
}

public class ConnectRequestDto
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string? Phone { get; private set; }
    public string? Message { get; private set; }
    public bool AttendsChurch { get; private set; }
    public DateTime SubmittedAtUtc { get; private set; }
    public bool IsRead { get; private set; }

    public ConnectRequestDto(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string? phone,
        string? message,
        bool attendsChurch,
        DateTime submittedAtUtc,
        bool isRead)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Message = message;
        AttendsChurch = attendsChurch;
        SubmittedAtUtc = submittedAtUtc;
        IsRead = isRead;
    }
}

public class ChurchSettingsDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Tagline { get; private set; } = default!;
    public string AboutHtml { get; private set; } = default!;
    public string HeroTitle { get; private set; } = default!;
    public string HeroSubtitle { get; private set; } = default!;
    public string? HeroVideoUrl { get; private set; }
    public string? LivestreamUrl { get; private set; }
    public string? GivingUrl { get; private set; }
    public string GivingHeadline { get; private set; } = default!;
    public string GivingBody { get; private set; } = default!;
    public string? ContactEmail { get; private set; }
    public string? ContactPhone { get; private set; }
    public string? LogoUrl { get; private set; }
    public string FooterNote { get; private set; } = default!;

    public ChurchSettingsDto(
        Guid id,
        string name,
        string tagline,
        string aboutHtml,
        string heroTitle,
        string heroSubtitle,
        string? heroVideoUrl,
        string? livestreamUrl,
        string? givingUrl,
        string givingHeadline,
        string givingBody,
        string? contactEmail,
        string? contactPhone,
        string? logoUrl,
        string footerNote)
    {
        Id = id;
        Name = name;
        Tagline = tagline;
        AboutHtml = aboutHtml;
        HeroTitle = heroTitle;
        HeroSubtitle = heroSubtitle;
        HeroVideoUrl = heroVideoUrl;
        LivestreamUrl = livestreamUrl;
        GivingUrl = givingUrl;
        GivingHeadline = givingHeadline;
        GivingBody = givingBody;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
        LogoUrl = logoUrl;
        FooterNote = footerNote;
    }
}
