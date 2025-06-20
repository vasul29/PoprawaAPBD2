﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoprawaAPBD2.Models;

[Table("Title")]
public class Title
{
   [Key]
   public int TitleId { get; set; }
   [MaxLength(100)]
   public string Name { get; set; } = null!;
   
   public virtual ICollection<CharacterTitle> CharacterTitles { get; set; } = null!;
}