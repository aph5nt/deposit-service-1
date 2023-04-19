using Newtonsoft.Json;

namespace DepositService.BlockExplorers.Ltc.Status;
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public record Backend(
    [property: JsonProperty("chain")] string Chain,
    [property: JsonProperty("blocks")] int Blocks,
    [property: JsonProperty("headers")] int Headers,
    [property: JsonProperty("bestBlockHash")] string BestBlockHash,
    [property: JsonProperty("difficulty")] string Difficulty,
    [property: JsonProperty("sizeOnDisk")] long SizeOnDisk,
    [property: JsonProperty("version")] string Version,
    [property: JsonProperty("subversion")] string Subversion,
    [property: JsonProperty("protocolVersion")] string ProtocolVersion,
    [property: JsonProperty("timeOffset")] int TimeOffset,
    [property: JsonProperty("warnings")] string Warnings
);

public record Blockbook(
    [property: JsonProperty("coin")] string Coin,
    [property: JsonProperty("host")] string Host,
    [property: JsonProperty("version")] string Version,
    [property: JsonProperty("gitCommit")] string GitCommit,
    [property: JsonProperty("buildTime")] DateTime BuildTime,
    [property: JsonProperty("syncMode")] bool SyncMode,
    [property: JsonProperty("initialSync")] bool InitialSync,
    [property: JsonProperty("inSync")] bool InSync,
    [property: JsonProperty("bestHeight")] ulong BestHeight,
    [property: JsonProperty("lastBlockTime")] string LastBlockTime,
    [property: JsonProperty("inSyncMempool")] bool InSyncMempool,
    [property: JsonProperty("lastMempoolTime")] string LastMempoolTime,
    [property: JsonProperty("mempoolSize")] int MempoolSize,
    [property: JsonProperty("decimals")] int Decimals,
    [property: JsonProperty("dbSize")] long DbSize,
    [property: JsonProperty("about")] string About
);

public record StatusResult(
    [property: JsonProperty("blockbook")] Blockbook Blockbook,
    [property: JsonProperty("backend")] Backend Backend
);