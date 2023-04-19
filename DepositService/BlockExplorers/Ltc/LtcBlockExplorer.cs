using DepositService.BlockExplorers.Ltc.Blocks;
using DepositService.BlockExplorers.Ltc.Status;
using DepositService.BlockExplorers.Ltc.Transactions;
using Newtonsoft.Json;
using RestSharp;

namespace DepositService.BlockExplorers.Ltc;

public class LtcBlockExplorer : IBlockExplorer
{
    private readonly RestClient _client;
    
    public LtcBlockExplorer(string url = "https://litecoinblockexplorer.net")
    {
        _client = new RestClient(url);
    }

    public StatusResult GetStatus()
    {
        var request = new RestRequest($"/api");
        var response = _client.Execute(request);

        if (!response.IsSuccessful)
            return null;

        var result = JsonConvert.DeserializeObject<StatusResult>(response.Content);
        
        return result;
    }
    
    public BlockResult GetBlock(string heightOrHash)
    {
        var request = new RestRequest($"/api/v2/block/{heightOrHash}");
        var response = _client.Execute(request);

        if (!response.IsSuccessful)
            return null;

        var result = JsonConvert.DeserializeObject<BlockResult>(response.Content);
        
        return result;
    }

    public TransactionResult? GetTransaction(string txId)
    {
        var request = new RestRequest($"/api/v2/tx/{txId}");
        var response = _client.Execute(request);

        if (!response.IsSuccessful)
            return null;

        var result = JsonConvert.DeserializeObject<TransactionResult>(response.Content);
        
        return result;
    }
    
}