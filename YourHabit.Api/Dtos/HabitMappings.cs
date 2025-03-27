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
            Descriptions = request.Descriptions,
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
            habit.Descriptions,
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
}
