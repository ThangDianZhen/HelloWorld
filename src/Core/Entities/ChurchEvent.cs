namespace Ignite.Core.Entities;

public class ChurchEvent
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public DateTime StartsAtUtc { get; set; }
    public DateTime? EndsAtUtc { get; set; }
    public string? LocationName { get; set; }
    public Guid? CampusId { get; set; }
    public Campus? Campus { get; set; }
    public string? ImageUrl { get; set; }
    public string? RegistrationUrl { get; set; }
    public bool IsPublished { get; set; } = true;

    public ChurchEvent()
    {
    }

    public ChurchEvent(Guid id)
    {
        Id = id;
    }
}
