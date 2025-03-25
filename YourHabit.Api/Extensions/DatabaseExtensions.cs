using Microsoft.EntityFrameworkCore;
using YourHabit.Api.Database;

namespace YourHabit.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            await dbContext.Database.MigrateAsync();

            app.Logger.LogInformation("Database migrations applied succesfully.");
        }
        catch (Exception e)
        {
            app.Logger.LogError(e, "An error has occured while applying migrations.");
            throw;
        }
    }
}
