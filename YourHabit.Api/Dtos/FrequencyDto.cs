using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos;

public sealed record FrequencyDto 
{
    public required FrequencyType Type { get; init; }

    public required int TimesPerPeriod { get; init; }
};
