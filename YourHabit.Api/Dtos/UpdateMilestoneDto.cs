﻿namespace YourHabit.Api.Dtos;

public sealed record UpdateMilestoneDto
{
    public required int Target { get; init; }
}
