using System.ComponentModel.DataAnnotations;

namespace DepositService.Data;

public class Cursor
{
    [Key]
    public string Network { get; set; }
    public ulong LastProcessedBlock { get; set; }
}