using System.Linq.Expressions;
using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos;

internal static class HabitQueries
{
    public static Expression<Func<Habit, HabitResponse>> ProjectToResponse()
    {
        return x => new HabitResponse(
            x.Id,
            x.Name,
            x.Description,
            x.Type,
            new FrequencyDto { Type = x.Frequency.Type, TimesPerPeriod = x.Frequency.TimesPerPeriod },
            new TargetDto{ Value = x.Target.Value, Unit = x.Target.Unit },
            x.Status,
            x.IsArchived,
            x.EndDate,
            x.Milestone != null 
                ? new MilestoneDto{ Target = x.Milestone.Target, Current = x.Milestone.Current } 
                : default,
            x.CreatedAtUtc,
            x.UpdatedAtUtc,
            x.LastCompletedAtUtc);
    }
}
