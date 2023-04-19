using DepositService.Data;
using Serilog;

namespace DepositService.Workers;

public class DebugWorker : IHostedService
{
    private readonly DataContextFactory _contextFactory;

    public DebugWorker(DataContextFactory contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var dataContext = _contextFactory.CreateDbContext();
        var customerAccounts = dataContext.CustomerAccounts.Take(10).ToList();
        foreach (var customer in customerAccounts)
        {
            Log.Information("Customer Id # {customer}; Address: {Address}", customer.Id, customer.Address);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}