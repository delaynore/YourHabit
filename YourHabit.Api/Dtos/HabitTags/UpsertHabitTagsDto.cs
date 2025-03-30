namespace YourHabit.Api.Dtos.HabitTags;

public record UpsertHabitTagsDto
{
    public required List<string> TagIds { get; init; }
}
