using System.ComponentModel.DataAnnotations;

namespace DepositService.Data;

public class DepositHistoryItem
{
    [Key]
    public string Id { get; set; }
    public string TxId { get; set; }
    public string Asset { get; set; }
    public string Network { get; set; }
    public string Address { get; set; }
    public long Amount { get; set; }
    public int Confirmations { get; set; }
    public bool IsConfirmed { get; set; }
    public ulong BlockHeight { get; set; }
    public decimal AmountDecimal { get; set; }
}
 