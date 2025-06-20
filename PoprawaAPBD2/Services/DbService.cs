using Microsoft.EntityFrameworkCore;
using PoprawaAPBD2.Data;
using PoprawaAPBD2.Exceptions;
using PoprawaAPBD2.Models;
using PoprawaAPBD2.Models.DTOs;

namespace PoprawaAPBD2.Services;

public interface IDbService
{
    public Task<GetCharacterDTO> GetCharacterByIdAsync(int id);
    public Task<GetCharacterDTO> AddItemsToBackpackAsync(int id, List<int> itemIds);
}
public class DbService(AppDbContext dbContext) : IDbService
{
       public async Task<GetCharacterDTO> GetCharacterByIdAsync(int id)
    {
        var character = await dbContext.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(b => b.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Title)
            .FirstOrDefaultAsync(c => c.CharacterId == id);

        if (character == null)
            throw new NotFoundException($"Character with id {id} not found");

        return new GetCharacterDTO
        {
            CharacterId = character.CharacterId,
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            BackpackItems = character.Backpacks.Select(b => new BackpackItemDTO
            {
                ItemName = b.Item.Name,
                ItemWeight = b.Item.Weight,
                Amount = b.Amount
            }).ToList(),
            Titles = character.CharacterTitles.Select(t => new TitleDTO
            {
                Title = t.Title.Name,
                AquiredAt = t.AcquiredAt
            }).ToList()
        };
    }

    public async Task<GetCharacterDTO> AddItemsToBackpackAsync(int id, List<int> itemIds)
    {
        var character = await dbContext.Characters
            .Include(c => c.Backpacks)
            .ThenInclude(b => b.Item)
            .FirstOrDefaultAsync(c => c.CharacterId == id);

        if (character == null)
            throw new NotFoundException($"Character with id {id} not found");

        var items = await dbContext.Items
            .Where(i => itemIds.Contains(i.ItemId))
            .ToListAsync();

        if (items.Count != itemIds.Count)
            throw new NotFoundException("One or more items not found");

        int totalNewWeight = items.Sum(i => i.Weight);
        if (character.CurrentWeight + totalNewWeight > character.MaxWeight)
            throw new MaxWeightException("Character cannot carry that much weight");

        foreach (var item in items)
        {
            var backpackEntry = character.Backpacks.FirstOrDefault(b => b.ItemId == item.ItemId);
            if (backpackEntry != null)
            {
                backpackEntry.Amount += 1;
            }
            else
            {
                dbContext.Backpacks.Add(new Backpack
                {
                    CharacterId = character.CharacterId,
                    ItemId = item.ItemId,
                    Amount = 1
                });
            }
            character.CurrentWeight += item.Weight;
        }

        await dbContext.SaveChangesAsync();

        return await GetCharacterByIdAsync(id);
    }
    
}