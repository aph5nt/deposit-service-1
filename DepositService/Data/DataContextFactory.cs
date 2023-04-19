using Microsoft.EntityFrameworkCore;

namespace DepositService.Data;

public class DataContextFactory 
{
    private readonly string _connectionString;

    public DataContextFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
        
    public DataContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite(_connectionString);
        return new DataContext(optionsBuilder.Options);
    }
}