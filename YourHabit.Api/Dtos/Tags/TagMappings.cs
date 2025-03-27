using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos.Tags;

public static class TagMappings
{
    public static TagResponse ToResponse(this Tag tag)
    {
        return new TagResponse(tag.Id,
            tag.Name,
            tag.Description,
            tag.CreatedAtUtc,
            tag.UpdatedAtUtc);
    }

    public static Tag ToEntity(this CreateTagRequest request)
    {
        return new Tag
        {
            Id = $"t_{Guid.CreateVersion7()}",
            Name = request.Name,
            Description = request.Description,
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    public static void UpdateFromRequest(this Tag tag, UpdateTagRequest request)
    {
        tag.Name = request.Name;
        tag.Description = request.Description;
        tag.UpdatedAtUtc = DateTime.UtcNow;
    }
}
