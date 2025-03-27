using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourHabit.Api.Database;
using YourHabit.Api.Dtos;

namespace YourHabit.Api.Controllers;

[ApiController]
[Route("habits")]
public sealed class HabitsController(ApplicationDbContext dbContext) : ControllerBase
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    [HttpGet]
    public async Task<Ok<HabitsCollectionResponse>> GetHabits()
    {
        var habits = await _dbContext.Habits
            .Select(x => new HabitResponse(
                x.Id,
                x.Name,
                x.Descriptions,
                x.Type,
                new FrequencyDto(x.Frequency.Type, x.Frequency.TimesPerPeriod),
                new TargetDto(x.Target.Value, x.Target.Unit),
                x.Status,
                x.IsArchived,
                x.EndDate,
                x.Milestone != null ? new MilestoneDto(x.Milestone.Target, x.Milestone.Current) : default,
                x.CreatedAtUtc,
                x.UpdatedAtUtc,
                x.LastCompletedAtUtc))
            .ToListAsync();

        return TypedResults.Ok(new HabitsCollectionResponse(habits));
    }

    [HttpGet("{id}")]
    public async Task<Results<Ok<HabitResponse>, NotFound>> GetHabit(string id)
    {
        var habit = await _dbContext.Habits
            .Where(x => x.Id == id)
            .Select(x => new HabitResponse(
                x.Id,
                x.Name,
                x.Descriptions,
                x.Type,
                new FrequencyDto(x.Frequency.Type, x.Frequency.TimesPerPeriod),
                new TargetDto(x.Target.Value, x.Target.Unit),
                x.Status,
                x.IsArchived,
                x.EndDate,
                x.Milestone != null ? new MilestoneDto(x.Milestone.Target, x.Milestone.Current) : default,
                x.CreatedAtUtc,
                x.UpdatedAtUtc,
                x.LastCompletedAtUtc))
            .FirstOrDefaultAsync();

        if (habit is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(habit);
    }
}
