namespace Ignite.Core.Entities;

public class Ministry
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? LeaderName { get; set; }
    public bool IsPublished { get; set; } = true;
    public int SortOrder { get; set; }

    public Ministry()
    {
    }

    public Ministry(Guid id)
    {
        Id = id;
    }
}
