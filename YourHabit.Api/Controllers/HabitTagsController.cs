using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourHabit.Api.Database;
using YourHabit.Api.Dtos.HabitTags;

namespace YourHabit.Api.Controllers;

[ApiController]
[Route("habits/{habitId}/tags")]
public sealed class HabitTagsController(ApplicationDbContext dbContext) : ControllerBase
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    [HttpPut]
    public async Task<Results<Ok, NotFound, NoContent, BadRequest<string>>> UpsertHabitTags(
        [FromRoute] string habitId, 
        [FromBody] UpsertHabitTagsDto upsertHabitTagsDto)
    {
        var habit = await _dbContext.Habits
            .Include(x => x.HabitTags)
            .FirstOrDefaultAsync(x => x.Id == habitId);

        if (habit is null)
        {
            return TypedResults.NotFound();
        }

        var currentTagIds = habit.HabitTags.Select(x => x.TagId).ToHashSet();

        if (currentTagIds.SetEquals(upsertHabitTagsDto.TagIds))
        {
            return TypedResults.NoContent();
        }

        var existingTagIds = await _dbContext.Tags
            .Where(x => upsertHabitTagsDto.TagIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync();

        if(existingTagIds.Count != upsertHabitTagsDto.TagIds.Count)
        {
            return TypedResults.BadRequest("One or more tag IDs is invalid.");
        }

        habit.HabitTags.RemoveAll(x => !upsertHabitTagsDto.TagIds.Contains(x.TagId));

        var tagIdsToAdd = upsertHabitTagsDto.TagIds.Except(currentTagIds).ToArray();
        habit.HabitTags.AddRange(tagIdsToAdd.Select(tagId => new Entities.HabitTag
        {
            HabitId = habitId,
            TagId = tagId,
            CreatedAtUtc = DateTime.UtcNow
        }));

        await _dbContext.SaveChangesAsync();

        return TypedResults.Ok();
    }

    [HttpDelete("{tagId}")]
    public async Task<Results<NotFound, NoContent>> DeleteHabitTag(
        [FromRoute] string habitId, 
        [FromRoute] string tagId)
    {
        var habitTag = await _dbContext.HabitTags
            .SingleOrDefaultAsync(x => x.HabitId == habitId && x.TagId == tagId);

        if(habitTag is null)
        {
            return TypedResults.NotFound();
        }

        _dbContext.HabitTags.Remove(habitTag);

        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}
