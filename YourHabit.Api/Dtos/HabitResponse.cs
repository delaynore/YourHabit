using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos;

public sealed record HabitResponse(
    string Id,
    string Name,
    string? Descriptions,
    HabitType Type,
    FrequencyResponse Frequency,
    TargetResponse Target,
    HabitStatus Status,
    bool IsArchived,
    DateOnly? EndDate,
    MilestoneResponse? Milestone,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc,
    DateTime? LastCompletedAtUtc);
