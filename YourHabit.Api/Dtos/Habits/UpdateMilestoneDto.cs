namespace YourHabit.Api.Dtos.Habits;

public sealed record UpdateMilestoneDto
{
    public required int Target { get; init; }
}
