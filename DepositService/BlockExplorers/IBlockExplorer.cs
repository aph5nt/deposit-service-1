using DepositService.BlockExplorers.Ltc.Blocks;
using DepositService.BlockExplorers.Ltc.Status;
using DepositService.BlockExplorers.Ltc.Transactions;

namespace DepositService.BlockExplorers;
 
public interface IBlockExplorer
{
    StatusResult? GetStatus();
    BlockResult? GetBlock(string heightOrHash);
    
    TransactionResult? GetTransaction(string txId);
}