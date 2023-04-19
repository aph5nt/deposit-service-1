using DepositService.BlockExplorers.Ltc;
 
namespace DepositService.Tests;

public class LtcBlockExplorerTests
{
    private LtcBlockExplorer _blockExplorer;
    
    [SetUp]
    public void Setup()
    {
        _blockExplorer = new LtcBlockExplorer();
    }

    [Test]
    public void Should_get_block_by_height()
    {
        var block = _blockExplorer.GetBlock("1000");
        Assert.That(block.Height == 1000);
    }
    
    [Test]
    public void Should_get_status()
    {
        var status = _blockExplorer.GetStatus();
        Assert.True(status.Blockbook.InSync);
    }
    
    [Test]
    public void Should_get_transaction()
    {
        var txId = "aaf16a8ddec18be13dd0a2c0f3127e752e69a9f188eb2e6ef73a29bc89b4746d";
        var transaction = _blockExplorer.GetTransaction(txId);
        Assert.That(transaction.Txid == txId);
    }
}