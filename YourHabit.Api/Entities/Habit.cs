﻿namespace YourHabit.Api.Entities;

public sealed class Habit
{
    public string Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Descriptions { get; set; }

    public HabitType Type { get; set; }

    public Frequency Frequency { get; set; }

    public Target Target { get; set; }

    public HabitStatus Status { get; set; }

    public bool IsArchived { get; set; }

    public DateOnly? EndDate { get; set; }

    public Milestone? Milestone { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }

    public DateTime? LastCompletedAtUtc { get; set; }
}
