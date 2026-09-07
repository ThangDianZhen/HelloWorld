using Ignite.Core.Enums;

namespace Ignite.Core.Entities;

public class ServiceTime
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid CampusId { get; set; }
    public Campus Campus { get; set; } = default!;
    public ServiceDay Day { get; set; } = ServiceDay.Sunday;
    public string TimeLabel { get; set; } = default!;
    public string Title { get; set; } = "Main Service";
    public string? Notes { get; set; }
    public int SortOrder { get; set; }

    public ServiceTime()
    {
    }

    public ServiceTime(Guid id)
    {
        Id = id;
    }
}
