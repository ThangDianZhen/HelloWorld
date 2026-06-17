namespace Application.Dto.Entities;

public class GreetingDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Message { get; private set; } = default!;
    public DateTime CreatedAtUtc { get; private set; }

    public GreetingDto(Guid id, string name, string message, DateTime createdAtUtc)
    {
        Id = id;
        Name = name;
        Message = message;
        CreatedAtUtc = createdAtUtc;
    }
}
