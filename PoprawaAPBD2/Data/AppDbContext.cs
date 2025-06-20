using Microsoft.EntityFrameworkCore;
using PoprawaAPBD2.Models;

namespace PoprawaAPBD2.Data;

public class AppDbContext : DbContext
{
    public DbSet<Backpack> Backpacks { get; set; } = null!;
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<CharacterTitle> CharacterTitles { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Title> Titles { get; set; } = null!;

    protected AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Character>().HasData(
            new Character { CharacterId = 1, FirstName = "Arthas", LastName = "Menethil", CurrentWeight = 15, MaxWeight = 50 },
            new Character { CharacterId = 2, FirstName = "Jaina", LastName = "Proudmoore", CurrentWeight = 10, MaxWeight = 40 },
            new Character { CharacterId = 3, FirstName = "Thrall", LastName = "Doomhammer", CurrentWeight = 20, MaxWeight = 60 }
        );
        
        modelBuilder.Entity<Item>().HasData(
            new Item { ItemId = 1, Name = "Sword", Weight = 5 },
            new Item { ItemId = 2, Name = "Staff", Weight = 3 },
            new Item { ItemId = 3, Name = "Shield", Weight = 7 }
        );
        
        modelBuilder.Entity<Title>().HasData(
            new Title { TitleId = 1, Name = "Champion" },
            new Title { TitleId = 2, Name = "Warlord" },
            new Title { TitleId = 3, Name = "Archmage" }
        );
        
        modelBuilder.Entity<Backpack>().HasData(
            new Backpack { CharacterId = 1, ItemId = 1, Amount = 1 },
            new Backpack { CharacterId = 2, ItemId = 2, Amount = 2 },
            new Backpack { CharacterId = 3, ItemId = 3, Amount = 1 }
        );
        
        modelBuilder.Entity<CharacterTitle>().HasData(
            new CharacterTitle { CharacterId = 1, TitleId = 1, AcquiredAt = new DateTime(2023, 01, 01) },
            new CharacterTitle { CharacterId = 2, TitleId = 2, AcquiredAt = new DateTime(2023, 06, 15) },
            new CharacterTitle { CharacterId = 3, TitleId = 3, AcquiredAt = new DateTime(2024, 03, 10) }
        );
    }

    
}