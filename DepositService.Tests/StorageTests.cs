using DepositService.Data;

namespace DepositService.Tests;

public class StorageTests
{
    [Test]
    public void Should_store_deposit_history_item()
    {
        var connectionString = "Data Source=DepositServiceTest.db;";
        MigrationExtensions.RunDataContextMigrations<DataContext>(connectionString);

        var dataContextFactory = new DataContextFactory(connectionString);
        var dataContext = dataContextFactory.CreateDbContext();
                
        dataContext.DepositHistory.Add(new DepositHistoryItem
        {
            Id = "txid-0",
            Address = "Address",
            Amount = 100L,
            AmountDecimal = 100m,
            Asset = "ZEC",
            TxId = "txid",
            Network = "ZEC",
            Confirmations = 1
        });
        
        dataContext.SaveChanges();
        
        var dataContext2 = dataContextFactory.CreateDbContext();
        var depositHistoryItem2 = dataContext2.DepositHistory.SingleOrDefault(x => x.TxId == "txid");
        
        Assert.NotNull(depositHistoryItem2);
    }
}