using DepositService.BlockExplorers;
using DepositService.Data;

namespace DepositService.Workers;

public class LtcDepositConfirmationWorker : BackgroundService
{
    private const int AssetMinConfirmations = 10;
    
    private readonly DataContextFactory _contextFactory;
    private readonly IBlockExplorer _blockExplorer;

    public LtcDepositConfirmationWorker(DataContextFactory contextFactory, IBlockExplorer blockExplorer)
    {
        _contextFactory = contextFactory;
        _blockExplorer = blockExplorer;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(1000, stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoWork(stoppingToken);

            await Task.Delay(10000, stoppingToken);
        }
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        await using var dataContext = _contextFactory.CreateDbContext();
        var deposits = dataContext.DepositHistory
            .Where(x => !x.IsConfirmed)
            .ToList() // workaround for SQLLite limitations, for purposes of this task only
            .OrderBy(x => x.BlockHeight)
            .Take(100)
            .ToList();
     
        foreach (var deposit in deposits)
        {
            var transaction = _blockExplorer.GetTransaction(deposit.TxId);
            if (transaction != null)
            {
                deposit.Confirmations = transaction.Confirmations;
                if (deposit.Confirmations >= AssetMinConfirmations)
                {
                    deposit.IsConfirmed = true;
                    deposit.Confirmations = AssetMinConfirmations;
                }

                await dataContext.SaveChangesAsync(stoppingToken);
                // we could publish some events here, to other exchange systems
            }
        }
    }
}