namespace Ignite.Core.Entities;

public class Campus
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = default!;
    public string Address { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? MapUrl { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Description { get; set; }
    public bool IsOnlineCampus { get; set; }
    public bool IsPublished { get; set; } = true;
    public int SortOrder { get; set; }
    public ICollection<ServiceTime> ServiceTimes { get; set; } = new List<ServiceTime>();

    public Campus()
    {
    }

    public Campus(Guid id)
    {
        Id = id;
    }
}
