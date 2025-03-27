namespace YourHabit.Api.Dtos.Tags;

public sealed record TagResponse(
    string Id,
    string Name,
    string? Description,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);
