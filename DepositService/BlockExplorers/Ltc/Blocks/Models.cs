using Newtonsoft.Json;

namespace DepositService.BlockExplorers.Ltc.Blocks;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public record BlockResult(
        [property: JsonProperty("page")] int Page,
        [property: JsonProperty("totalPages")] int TotalPages,
        [property: JsonProperty("itemsOnPage")] int ItemsOnPage,
        [property: JsonProperty("hash")] string Hash,
        [property: JsonProperty("previousBlockHash")] string PreviousBlockHash,
        [property: JsonProperty("nextBlockHash")] string NextBlockHash,
        [property: JsonProperty("height")] ulong Height,
        [property: JsonProperty("confirmations")] int Confirmations,
        [property: JsonProperty("size")] int Size,
        [property: JsonProperty("time")] int Time,
        [property: JsonProperty("version")] int Version,
        [property: JsonProperty("merkleRoot")] string MerkleRoot,
        [property: JsonProperty("nonce")] string Nonce,
        [property: JsonProperty("bits")] string Bits,
        [property: JsonProperty("difficulty")] string Difficulty,
        [property: JsonProperty("txCount")] int TxCount,
        [property: JsonProperty("txs")] IReadOnlyList<Tx> Txs
    );

    public record Tx(
        [property: JsonProperty("txid")] string Txid,
        [property: JsonProperty("vin")] IReadOnlyList<Vin> Vin,
        [property: JsonProperty("vout")] IReadOnlyList<Vout> Vout,
        [property: JsonProperty("blockHash")] string BlockHash,
        [property: JsonProperty("blockHeight")] int BlockHeight,
        [property: JsonProperty("confirmations")] int Confirmations,
        [property: JsonProperty("blockTime")] int BlockTime,
        [property: JsonProperty("value")] string Value,
        [property: JsonProperty("valueIn")] string ValueIn,
        [property: JsonProperty("fees")] string Fees
    );

    public record Vin(
        [property: JsonProperty("n")] int N,
        [property: JsonProperty("value")] string Value,
        [property: JsonProperty("addresses")] IReadOnlyList<string> Addresses,
        [property: JsonProperty("isAddress")] bool? IsAddress
    );

    public record Vout(
        [property: JsonProperty("value")] string Value,
        [property: JsonProperty("n")] int N,
        [property: JsonProperty("addresses")] IReadOnlyList<string> Addresses,
        [property: JsonProperty("isAddress")] bool IsAddress,
        [property: JsonProperty("spent")] bool? Spent
    );