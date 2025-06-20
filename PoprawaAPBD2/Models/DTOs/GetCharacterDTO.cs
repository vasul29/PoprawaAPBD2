namespace PoprawaAPBD2.Models.DTOs;

public class GetCharacterDTO
{
    public int CharacterId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<BackpackItemDTO> BackpackItems { get; set; } = new();
    public List<TitleDTO> Titles { get; set; } = new();
}

public class BackpackItemDTO
{
    public string ItemName { get; set; } = null!;
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class TitleDTO
{
    public string Title { get; set; } = null!;
    public DateTime AquiredAt { get; set; }
}
