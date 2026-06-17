using Application.Dto.Entities;
using Application.Dto.Extensions;
using Application.Interfaces.Persistence.Repositories;
using Application.Interfaces.Services.Entities;
using Application.Results;
using Core.Entities;

namespace Application.Services.Entities;

public class GreetingService : IGreetingService
{
    private readonly IGreetingRepository _greetingRepository;

    public GreetingService(IGreetingRepository greetingRepository)
    {
        _greetingRepository = greetingRepository;
    }

    public async Task<Result<GreetingDto>> CreateGreetingAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<GreetingDto>.Failure("Name is required.");
        }

        var greeting = new Greeting
        {
            Name = name,
            Message = $"Hello, {name}!"
        };

        var saved = await _greetingRepository.AddAsync(greeting, cancellationToken);
        return Result<GreetingDto>.Success(saved.ToDto(), "Greeting created.");
    }

    public async Task<Result<IReadOnlyList<GreetingDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var greetings = await _greetingRepository.GetAllAsync(cancellationToken);
        var dtos = greetings.Select(g => g.ToDto()).ToList();
        return Result<IReadOnlyList<GreetingDto>>.Success(dtos);
    }
}
