using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourHabit.Api.Database;
using YourHabit.Api.Dtos.Habits;

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
            .Select(HabitQueries.ProjectToResponse())
            .ToListAsync();

        return TypedResults.Ok(new HabitsCollectionResponse(habits));
    }

    [HttpGet("{id}", Name = nameof(GetHabit))]
    public async Task<Results<Ok<HabitWithTagsResponse>, NotFound>> GetHabit([FromRoute] string id)
    {
        var habit = await _dbContext.Habits
            .Where(x => x.Id == id)
            .Select(HabitQueries.ProjectToHabitWithTagsResponse())
            .FirstOrDefaultAsync();

        if (habit is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(habit);
    }

    [HttpPost]
    public async Task<IResult> CreateHabit([FromBody] CreateHabitRequest request)
    {
        var habit = request.ToEntity();

        _dbContext.Add(habit);
        await _dbContext.SaveChangesAsync();

        var response = habit.ToResponse();

        return Results.CreatedAtRoute(nameof(GetHabit), new { response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<Results<NotFound, NoContent>> UpdateHabit([FromRoute] string id, [FromBody] UpdateHabitRequest request)
    {
        var habit = await _dbContext.Habits.FirstOrDefaultAsync(x => x.Id == id);

        if (habit is null)
        {
            return TypedResults.NotFound();
        }

        habit.UpdateFromRequest(request);

        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<Results<NotFound, NoContent>> DeleteHabit([FromRoute] string id)
    {
        var habit = await _dbContext.Habits.FirstOrDefaultAsync(x => x.Id == id);

        if (habit is null)
        {
            return TypedResults.NotFound();
        }

        _dbContext.Remove(habit);
        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }


}
