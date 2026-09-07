namespace Ignite.Core.Entities;

public class Page
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string BodyHtml { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public bool IsPublished { get; set; } = true;
    public int SortOrder { get; set; }
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    public Page()
    {
    }

    public Page(Guid id)
    {
        Id = id;
    }
}
