using DepositService.BlockExplorers;
using DepositService.Data;
using Serilog;

namespace DepositService.Workers;

public class LtcDepositHistoryWorker : BackgroundService
{
    private const string AssetNetwork = "LTC";
    private const int AssetScale = 8;
    
    private readonly DataContextFactory _contextFactory;
    private readonly IBlockExplorer _blockExplorer;
   
    public LtcDepositHistoryWorker(DataContextFactory contextFactory, IBlockExplorer blockExplorer)
    {
        _contextFactory = contextFactory;
        _blockExplorer = blockExplorer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Starting {AssetNetwork} deposit worker", AssetNetwork);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoWork(stoppingToken);

            await Task.Delay(10000, stoppingToken);
        }
        
        Log.Information("Exiting {AssetNetwork} deposit worker", AssetNetwork);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        var status = _blockExplorer.GetStatus();
        if (status == null)
        {
            Log.Error("Failed to get block explorer node status");
            return;
        }

        await using var dataContext = _contextFactory.CreateDbContext();
        var cursor = dataContext.Cursors.Single(x => x.Network == AssetNetwork);
        var currentNetworkBlockHeight = status.Blockbook.BestHeight - 1; // to skip 0 confirms
        var blockHeightToProcess = cursor.LastProcessedBlock + 1L;

        if (currentNetworkBlockHeight == cursor.LastProcessedBlock) return;
        
        Log.Information("Processing block {Block}", blockHeightToProcess);
        var block = _blockExplorer.GetBlock(blockHeightToProcess.ToString());

        if (block == null)
        {
            Log.Warning("Block not found");
            return;
        }
        
        foreach (var transaction in block.Txs)
        {
            foreach (var vOut in transaction.Vout)
            {
                if(!vOut.IsAddress) continue;

                var address = vOut.Addresses.FirstOrDefault();
                if(address == null) continue;
                
                // is this deposit to our address? if not skip
                var existingAccount = dataContext.CustomerAccounts.SingleOrDefault(x => x.Network == AssetNetwork && x.Address.ToLower() == address.ToLower());
                if (existingAccount == null)
                    continue;
                
                if (dataContext.DepositHistory.Any(x => x.Network == AssetNetwork && x.TxId == transaction.Txid))
                {
                    Log.Warning("Transaction {TxId} was processed already, skipping", transaction.Txid);
                    continue;
                }

                var depositHistoryItem = new DepositHistoryItem
                {
                    Id = $"{transaction.Txid}-{vOut.N}",
                    Address = address.ToLower(),
                    Amount = long.Parse(vOut.Value),
                    AmountDecimal = decimal.Parse(vOut.Value) / AssetScale,
                    Asset = AssetNetwork,
                    TxId = transaction.Txid,
                    Network = AssetNetwork,
                    Confirmations = block.Confirmations
                };
                
                dataContext.DepositHistory.Add(depositHistoryItem);
                await dataContext.SaveChangesAsync(stoppingToken);
                
                Log.Debug("Added deposit item {DepositHistory}", depositHistoryItem);
            }
        }
        
        cursor.LastProcessedBlock = block.Height;
        await dataContext.SaveChangesAsync(stoppingToken);
    }
}
