using System.Linq.Expressions;
using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos.Tags;

internal static class TagQueries
{
    public static Expression<Func<Tag, TagResponse>> ProjectToDto()
    {
        return x => new TagResponse(x.Id,
            x.Name,
            x.Description,
            x.CreatedAtUtc,
            x.UpdatedAtUtc);
    }
}
