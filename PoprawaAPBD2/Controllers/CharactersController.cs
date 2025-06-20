using Microsoft.AspNetCore.Mvc;
using PoprawaAPBD2.Exceptions;
using PoprawaAPBD2.Services;

namespace PoprawaAPBD2.Controllers;

[ApiController]
[Route("api/characters")]
public class CharactersController(IDbService dbService) : ControllerBase
{
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetCharacterById([FromRoute] int id)
    {
        try
        {
            var character = await dbService.GetCharacterByIdAsync(id);
            return Ok(character);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    [Route("{id}/backpacks")]
    public async Task<IActionResult> AddItemsToBackpack([FromRoute] int id, [FromBody] List<int> itemIds)
    {
        if (itemIds.Count == 0)
            return BadRequest("List of item IDs can't be empty");
        try
        {
            var characterWithItems = await dbService.AddItemsToBackpackAsync(id, itemIds);
            return CreatedAtAction(nameof(GetCharacterById), new {id = characterWithItems.CharacterId}, characterWithItems);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (MaxWeightException e)
        {
            return BadRequest(e.Message);
        }
    }
}