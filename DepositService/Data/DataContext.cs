using DepositService.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DepositService.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DataContext()
    {
    }

    public DbSet<DepositHistoryItem> DepositHistory { get; set; }
    public DbSet<CustomerAccount> CustomerAccounts { get; set; }
    public DbSet<Cursor> Cursors { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerAccount>(entity =>
        {  
            entity.ToTable("CustomerAccounts");
            entity.Property(x => x.Id).IsRequired();
            entity.Property(x => x.Name).HasMaxLength(255).IsRequired();
            entity.Property(x => x.Asset).HasMaxLength(16).IsRequired();
            entity.Property(x => x.Network).HasMaxLength(16).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(255).IsRequired();
            entity.HasIndex(x => new { x.Address, x.Asset, x.Network });
        });
        
        modelBuilder.Entity<DepositHistoryItem>(entity =>
        {
            entity.ToTable("DepositHistory");
            entity.Property(x => x.Id).HasMaxLength(255).IsRequired();
            entity.Property(x => x.TxId).HasMaxLength(255).IsRequired();
            entity.Property(x => x.Asset).HasMaxLength(16).IsRequired();
            entity.Property(x => x.Network).HasMaxLength(16).IsRequired();
            entity.Property(x => x.Amount).HasPrecision(6, 18).IsRequired();
            entity.Property(x => x.Confirmations).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(255).IsRequired();
            entity.Property(x => x.IsConfirmed).IsRequired().HasDefaultValue(false);
            entity.Property(x => x.AmountDecimal).IsRequired().HasPrecision(12, 8);
            entity.Property(x => x.Address).HasMaxLength(255).IsRequired();
            entity.HasIndex(x => new { x.Address, x.Asset, x.Network });
            entity.HasIndex(x => x.Confirmations);
            entity.HasIndex(x => x.IsConfirmed);
        });

        // Hardcoded passphrase is intended, in regular app it would not be here
        var numberOfCustomers = 10000;
        var addresses =  LtcAddressGenerator.Generate(
                numberOfCustomers, 
                "fluid debris horror real rescue series sun fork kitchen problem cat skirt city stay athlete start skate hurdle spider hybrid cream net adjust charge")
            .Select((value, index) => (value, index))
            .ToDictionary(pair => pair.index, pair => pair.value);
        
        var customers = new List<CustomerAccount>();
        for (int i = 0; i < numberOfCustomers; i++)
        {
            customers.Add(new CustomerAccount
            {
                Address = addresses[i],
                Asset = "LTC",
                Network = "LTC",
                Id = i+1,
                Name = $"Customer {i}"
            });
        }

        modelBuilder.Entity<CustomerAccount>().HasData(customers);
        modelBuilder.Entity<Cursor>().HasData(new List<Cursor>()
        {
            new()
            {
                Network = "LTC",
                LastProcessedBlock = 2459289
            }
        });
    }
}
