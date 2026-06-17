using Application.Dto.Entities;
using Core.Entities;

namespace Application.Dto.Extensions;

public static class GreetingExtensions
{
    public static GreetingDto ToDto(this Greeting greeting)
        => new(greeting.Id, greeting.Name, greeting.Message, greeting.CreatedAtUtc);

    public static Greeting ToEntity(this GreetingDto dto)
        => new()
        {
            Name = dto.Name,
            Message = dto.Message
        };
}
