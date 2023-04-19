using Newtonsoft.Json;

namespace DepositService.BlockExplorers.Ltc.Transactions;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public record TransactionResult(
    [property: JsonProperty("txid")] string Txid,
    [property: JsonProperty("version")] int Version,
    [property: JsonProperty("vin")] IReadOnlyList<Vin> Vin,
    [property: JsonProperty("vout")] IReadOnlyList<Vout> Vout,
    [property: JsonProperty("blockHash")] string BlockHash,
    [property: JsonProperty("blockHeight")] int BlockHeight,
    [property: JsonProperty("confirmations")] int Confirmations,
    [property: JsonProperty("blockTime")] int BlockTime,
    [property: JsonProperty("size")] int Size,
    [property: JsonProperty("vsize")] int Vsize,
    [property: JsonProperty("value")] string Value,
    [property: JsonProperty("valueIn")] string ValueIn,
    [property: JsonProperty("fees")] string Fees,
    [property: JsonProperty("hex")] string Hex
);

public record Vin(
    [property: JsonProperty("txid")] string Txid,
    [property: JsonProperty("vout")] int Vout,
    [property: JsonProperty("sequence")] long Sequence,
    [property: JsonProperty("n")] int N,
    [property: JsonProperty("addresses")] IReadOnlyList<string> Addresses,
    [property: JsonProperty("isAddress")] bool IsAddress,
    [property: JsonProperty("value")] string Value,
    [property: JsonProperty("hex")] string Hex
);

public record Vout(
    [property: JsonProperty("value")] string Value,
    [property: JsonProperty("n")] int N,
    [property: JsonProperty("hex")] string Hex,
    [property: JsonProperty("addresses")] IReadOnlyList<string> Addresses,
    [property: JsonProperty("isAddress")] bool IsAddress
);
