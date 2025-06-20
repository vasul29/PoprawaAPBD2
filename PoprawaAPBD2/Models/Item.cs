using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoprawaAPBD2.Models;

[Table("Item")]
public class Item
{
    [Key]
    public int ItemId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public int Weight { get; set; }
    
    public virtual ICollection<Backpack> Backpacks { get; set; } = null!;
}