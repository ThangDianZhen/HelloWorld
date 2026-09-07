namespace Ignite.Core.Entities;

public class ChurchSettings
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
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

    public ChurchSettings()
    {
    }

    public ChurchSettings(Guid id)
    {
        Id = id;
    }
}
