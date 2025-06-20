using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoprawaAPBD2.Models;

[Table("Backpack")]
[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class Backpack
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
    
    [ForeignKey(nameof(ItemId))]
    public virtual Item Item { get; set; } = null!;
    [ForeignKey(nameof(CharacterId))]
    public virtual Character Character { get; set; } = null!;
    
}