﻿namespace YourHabit.Api.Entities;

public sealed class Tag
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }
}
