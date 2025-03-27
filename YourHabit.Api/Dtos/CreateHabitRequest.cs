using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos;

public sealed record CreateHabitRequest
{
    public required string Name { get; init; }

    public string? Descriptions { get; init; }

    public required HabitType Type { get; init; }

    public required FrequencyDto Frequency { get; init; }

    public required TargetDto Target { get; init; }

    public DateOnly? EndDate { get; init; }

    public MilestoneDto? Milestone { get; init; }
}
