namespace Core.Entities;

public class Greeting
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}
