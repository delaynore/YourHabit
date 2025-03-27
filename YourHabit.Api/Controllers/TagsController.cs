using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourHabit.Api.Database;
using YourHabit.Api.Dtos.Tags;

namespace YourHabit.Api.Controllers;

[ApiController]
[Route("tags")]
public sealed class TagsController(ApplicationDbContext dbContext) : ControllerBase
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    [HttpGet]
    public async Task<Ok<TagsCollectionResponse>> GetTags()
    {
        var tags = await _dbContext.Tags
            .Select(TagQueries.ProjectToDto())
            .ToListAsync();

        return TypedResults.Ok(new TagsCollectionResponse(tags));
    }

    [HttpGet("{id}", Name = nameof(GetTag))]
    public async Task<Results<Ok<TagResponse>, NotFound>> GetTag([FromRoute] string id)
    {
        var tag = await _dbContext.Tags
            .Where(x => x.Id == id)
            .Select(TagQueries.ProjectToDto())
            .FirstOrDefaultAsync();

        if (tag is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(tag);
    }

    [HttpPost]
    public async Task<Results<CreatedAtRoute<TagResponse>, Conflict<string>>> CreateTag([FromBody] CreateTagRequest request)
    {
        var tag = request.ToEntity();

        if (await _dbContext.Tags.AnyAsync(x => x.Name == tag.Name))
        {
            return TypedResults.Conflict($"The tag '{tag.Name}' already exists.");
        }

        _dbContext.Add(tag);
        await _dbContext.SaveChangesAsync();

        var response = tag.ToResponse();

        return TypedResults.CreatedAtRoute(response, nameof(GetTag), new { response.Id });
    }

    [HttpPut("{id}")]
    public async Task<Results<NoContent, NotFound>> CreateTag([FromRoute] string id, [FromBody] UpdateTagRequest request)
    {
        var tag = await _dbContext.Tags.FirstOrDefaultAsync(x=>x.Id == id);

        if (tag is null)
        {
            return TypedResults.NotFound();
        }

        tag.UpdateFromRequest(request);

        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<Results<NotFound, NoContent>> DeleteTag([FromRoute] string id)
    {
        var tag = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

        if (tag is null)
        {
            return TypedResults.NotFound();
        }

        _dbContext.Remove(tag);
        await _dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }
}
