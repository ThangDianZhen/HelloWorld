using Ignite.Application.Dto.Application;
using Ignite.Application.Dto.Entities;
using Ignite.Core.Entities;

namespace Ignite.Application.Dto.Extensions;

public static class EntityDtoExtensions
{
    public static ServiceTimeDto ToDto(this ServiceTime time)
        => new(time.Id, time.Day, time.TimeLabel, time.Title, time.Notes, time.SortOrder);

    public static CampusDto ToDto(this Campus campus)
    {
        var dto = new CampusDto(
            campus.Id,
            campus.Name,
            campus.Slug,
            campus.City,
            campus.Region,
            campus.Country,
            campus.Address,
            campus.ImageUrl,
            campus.MapUrl,
            campus.ContactEmail,
            campus.ContactPhone,
            campus.Description,
            campus.IsOnlineCampus,
            campus.IsPublished,
            campus.SortOrder);

        dto.ServiceTimes = campus.ServiceTimes
            .OrderBy(t => t.SortOrder)
            .ThenBy(t => t.Day)
            .Select(t => t.ToDto())
            .ToList();

        return dto;
    }

    public static SermonDto ToDto(this Sermon sermon)
        => new(
            sermon.Id,
            sermon.Title,
            sermon.Speaker,
            sermon.Series,
            sermon.PreachedOn,
            sermon.VideoUrl,
            sermon.AudioUrl,
            sermon.Description,
            sermon.ThumbnailUrl,
            sermon.CampusId,
            sermon.Campus?.Name,
            sermon.IsPublished);

    public static ChurchEventDto ToDto(this ChurchEvent churchEvent)
        => new(
            churchEvent.Id,
            churchEvent.Title,
            churchEvent.Slug,
            churchEvent.Description,
            churchEvent.StartsAtUtc,
            churchEvent.EndsAtUtc,
            churchEvent.LocationName,
            churchEvent.CampusId,
            churchEvent.Campus?.Name,
            churchEvent.ImageUrl,
            churchEvent.RegistrationUrl,
            churchEvent.IsPublished);

    public static PageDto ToDto(this Page page)
        => new(
            page.Id,
            page.Title,
            page.Slug,
            page.BodyHtml,
            page.Summary,
            page.IsPublished,
            page.SortOrder,
            page.UpdatedAtUtc);

    public static MinistryDto ToDto(this Ministry ministry)
        => new(
            ministry.Id,
            ministry.Name,
            ministry.Slug,
            ministry.Description,
            ministry.ImageUrl,
            ministry.LeaderName,
            ministry.IsPublished,
            ministry.SortOrder);

    public static ConnectRequestDto ToDto(this ConnectRequest request)
        => new(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Email,
            request.Phone,
            request.Message,
            request.AttendsChurch,
            request.SubmittedAtUtc,
            request.IsRead);

    public static ChurchSettingsDto ToDto(this ChurchSettings settings)
        => new(
            settings.Id,
            settings.Name,
            settings.Tagline,
            settings.AboutHtml,
            settings.HeroTitle,
            settings.HeroSubtitle,
            settings.HeroVideoUrl,
            settings.LivestreamUrl,
            settings.GivingUrl,
            settings.GivingHeadline,
            settings.GivingBody,
            settings.ContactEmail,
            settings.ContactPhone,
            settings.LogoUrl,
            settings.FooterNote);

    public static void Apply(this Campus campus, CampusWriteDto dto, string slug)
    {
        campus.Name = dto.Name.Trim();
        campus.Slug = slug;
        campus.City = dto.City.Trim();
        campus.Region = dto.Region.Trim();
        campus.Country = dto.Country.Trim();
        campus.Address = dto.Address.Trim();
        campus.ImageUrl = NullIfEmpty(dto.ImageUrl);
        campus.MapUrl = NullIfEmpty(dto.MapUrl);
        campus.ContactEmail = NullIfEmpty(dto.ContactEmail);
        campus.ContactPhone = NullIfEmpty(dto.ContactPhone);
        campus.Description = NullIfEmpty(dto.Description);
        campus.IsOnlineCampus = dto.IsOnlineCampus;
        campus.IsPublished = dto.IsPublished;
        campus.SortOrder = dto.SortOrder;
    }

    public static void Apply(this Sermon sermon, SermonWriteDto dto)
    {
        sermon.Title = dto.Title.Trim();
        sermon.Speaker = dto.Speaker.Trim();
        sermon.Series = NullIfEmpty(dto.Series);
        sermon.PreachedOn = dto.PreachedOn;
        sermon.VideoUrl = NullIfEmpty(dto.VideoUrl);
        sermon.AudioUrl = NullIfEmpty(dto.AudioUrl);
        sermon.Description = NullIfEmpty(dto.Description);
        sermon.ThumbnailUrl = NullIfEmpty(dto.ThumbnailUrl);
        sermon.CampusId = dto.CampusId;
        sermon.IsPublished = dto.IsPublished;
    }

    public static void Apply(this ChurchEvent churchEvent, ChurchEventWriteDto dto, string slug)
    {
        churchEvent.Title = dto.Title.Trim();
        churchEvent.Slug = slug;
        churchEvent.Description = dto.Description.Trim();
        churchEvent.StartsAtUtc = dto.StartsAtUtc;
        churchEvent.EndsAtUtc = dto.EndsAtUtc;
        churchEvent.LocationName = NullIfEmpty(dto.LocationName);
        churchEvent.CampusId = dto.CampusId;
        churchEvent.ImageUrl = NullIfEmpty(dto.ImageUrl);
        churchEvent.RegistrationUrl = NullIfEmpty(dto.RegistrationUrl);
        churchEvent.IsPublished = dto.IsPublished;
    }

    public static void Apply(this Page page, PageWriteDto dto, string slug)
    {
        page.Title = dto.Title.Trim();
        page.Slug = slug;
        page.BodyHtml = dto.BodyHtml;
        page.Summary = NullIfEmpty(dto.Summary);
        page.IsPublished = dto.IsPublished;
        page.SortOrder = dto.SortOrder;
        page.UpdatedAtUtc = DateTime.UtcNow;
    }

    public static void Apply(this Ministry ministry, MinistryWriteDto dto, string slug)
    {
        ministry.Name = dto.Name.Trim();
        ministry.Slug = slug;
        ministry.Description = dto.Description.Trim();
        ministry.ImageUrl = NullIfEmpty(dto.ImageUrl);
        ministry.LeaderName = NullIfEmpty(dto.LeaderName);
        ministry.IsPublished = dto.IsPublished;
        ministry.SortOrder = dto.SortOrder;
    }

    public static void Apply(this ChurchSettings settings, ChurchSettingsWriteDto dto)
    {
        settings.Name = dto.Name.Trim();
        settings.Tagline = dto.Tagline.Trim();
        settings.AboutHtml = dto.AboutHtml;
        settings.HeroTitle = dto.HeroTitle.Trim();
        settings.HeroSubtitle = dto.HeroSubtitle.Trim();
        settings.HeroVideoUrl = NullIfEmpty(dto.HeroVideoUrl);
        settings.LivestreamUrl = NullIfEmpty(dto.LivestreamUrl);
        settings.GivingUrl = NullIfEmpty(dto.GivingUrl);
        settings.GivingHeadline = dto.GivingHeadline.Trim();
        settings.GivingBody = dto.GivingBody;
        settings.ContactEmail = NullIfEmpty(dto.ContactEmail);
        settings.ContactPhone = NullIfEmpty(dto.ContactPhone);
        settings.LogoUrl = NullIfEmpty(dto.LogoUrl);
        settings.FooterNote = dto.FooterNote.Trim();
    }

    private static string? NullIfEmpty(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
