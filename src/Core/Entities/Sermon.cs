namespace Ignite.Core.Entities;

public class Sermon
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; set; } = default!;
    public string Speaker { get; set; } = default!;
    public string? Series { get; set; }
    public DateTime PreachedOn { get; set; } = DateTime.UtcNow.Date;
    public string? VideoUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? Description { get; set; }
    public string? ThumbnailUrl { get; set; }
    public Guid? CampusId { get; set; }
    public Campus? Campus { get; set; }
    public bool IsPublished { get; set; } = true;

    public Sermon()
    {
    }

    public Sermon(Guid id)
    {
        Id = id;
    }
}
