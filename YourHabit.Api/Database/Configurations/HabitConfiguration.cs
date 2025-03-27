using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourHabit.Api.Entities;

namespace YourHabit.Api.Database.Configurations;

public sealed class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(500);

        builder.Property(x => x.Name).HasMaxLength(100);

        builder.Property(x => x.Description).HasMaxLength(500);

        builder.OwnsOne(x => x.Frequency);
        builder.OwnsOne(x => x.Target, targetBuilder =>
        {
            targetBuilder.Property(t => t.Unit).HasMaxLength(100);
        });
        builder.OwnsOne(x => x.Milestone);
    }
}
