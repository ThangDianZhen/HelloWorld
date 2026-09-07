namespace Ignite.Core.Entities;

public class ConnectRequest
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; }
    public string? Message { get; set; }
    public bool AttendsChurch { get; set; }
    public DateTime SubmittedAtUtc { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }

    public ConnectRequest()
    {
    }

    public ConnectRequest(Guid id)
    {
        Id = id;
    }
}
