using Microsoft.EntityFrameworkCore;

namespace DepositService.Data;

public static class MigrationExtensions
{
    public static void RunDataContextMigrations<TContext>(string connectionString) where TContext : DbContext
    {
        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        optionsBuilder.UseSqlite(connectionString);
        var instance = (TContext) Activator.CreateInstance(typeof(TContext), optionsBuilder.Options);
        instance.Database.Migrate();
    }
}