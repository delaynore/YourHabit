using YourHabit.Api.Entities;

namespace YourHabit.Api.Dtos;

public sealed record FrequencyDto(
    FrequencyType Type,
    int TimesPerPeriod);
