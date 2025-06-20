using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoprawaAPBD2.Models;

[Table("CharacterTitle")]
[PrimaryKey(nameof(CharacterId), nameof(TitleId))]
public class CharacterTitle
{
    public int CharacterId { get; set; }
    public int TitleId { get; set; }
    public DateTime AcquiredAt { get; set; }
    
    [ForeignKey(nameof(CharacterId))]
    public virtual Character Character { get; set; } = null!;
    [ForeignKey(nameof(TitleId))]
    public virtual Title Title { get; set; } = null!;
}