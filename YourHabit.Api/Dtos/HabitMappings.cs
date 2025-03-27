using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos;

internal static class HabitMappings
{
    public static Habit ToEntity(this CreateHabitRequest request)
    {
        return new Habit
        {
            Id = $"h_{Guid.CreateVersion7()}",
            Name = request.Name,
            Description = request.Description,
            Type = request.Type,
            Frequency = new Frequency
            {
                Type = request.Frequency.Type,
                TimesPerPeriod = request.Frequency.TimesPerPeriod,
            },
            Target = new Target
            {
                Unit = request.Target.Unit,
                Value = request.Target.Value,
            },
            Status = HabitStatus.Ongoing,
            IsArchived = false,
            EndDate = request.EndDate,
            Milestone = request.Milestone is not null
                ? new Milestone
                {
                    Current = 0,
                    Target = request.Milestone.Target,
                }
                : null,
            CreatedAtUtc = DateTime.UtcNow,
            LastCompletedAtUtc = default,
            UpdatedAtUtc = default
        };
    }

    public static HabitResponse ToResponse(this Habit habit)
    {
        return new HabitResponse(
            habit.Id,
            habit.Name,
            habit.Description,
            habit.Type,
            new FrequencyDto{ Type = habit.Frequency.Type, TimesPerPeriod = habit.Frequency.TimesPerPeriod },
            new TargetDto{ Value = habit.Target.Value, Unit = habit.Target.Unit },
            habit.Status,
            habit.IsArchived,
            habit.EndDate,
            habit.Milestone != null
                ? new MilestoneDto{ Target = habit.Milestone.Target, Current = habit.Milestone.Current }
                : default,
            habit.CreatedAtUtc,
            habit.UpdatedAtUtc,
            habit.LastCompletedAtUtc);
    }

    public static void UpdateFromRequest(this Habit habit, UpdateHabitRequest request)
    {
        habit.Name = request.Name;
        habit.Description = request.Description;
        habit.Type = request.Type;
        habit.EndDate = request.EndDate;

        habit.Frequency = new Frequency
        {
            Type = request.Frequency.Type,
            TimesPerPeriod = request.Frequency.TimesPerPeriod
        };

        habit.Target = new Target
        {
            Value = request.Target.Value,
            Unit = request.Target.Unit
        };

        if (request.Milestone is { })
        {
            habit.Milestone ??= new Milestone();
            habit.Milestone.Target = request.Milestone.Target;
        }

        habit.UpdatedAtUtc = DateTime.UtcNow;
    }
}
