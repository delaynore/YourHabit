using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos;

public sealed record HabitResponse(
    string Id,
    string Name,
    string? Description,
    HabitType Type,
    FrequencyDto Frequency,
    TargetDto Target,
    HabitStatus Status,
    bool IsArchived,
    DateOnly? EndDate,
    MilestoneDto? Milestone,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc,
    DateTime? LastCompletedAtUtc);
