using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos;

public sealed record FrequencyResponse(
    FrequencyType Type,
    int TimesPerPeriod);
