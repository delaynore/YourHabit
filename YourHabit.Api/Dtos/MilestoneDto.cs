﻿namespace YourHabit.Api.Dtos;

public sealed record MilestoneDto
{
    public required int Target { get; init; }

    public required int Current { get; init; }
}
