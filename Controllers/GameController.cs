using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController(IGameService gameService) : ControllerBase
{
  private readonly IGameService _gameService = gameService;

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Models.Game>>> GetAllGames([FromQuery] string? search = null)
  {
    try
    {
      var games = await _gameService.GetAllGamesAsync(search);
      return Ok(games);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"An error occurred while retrieving games: {ex.Message}");
    }
  }

  [HttpGet("{appId}")]
  public async Task<ActionResult<Models.Game>> GetGameByAppId(int appId)
  {
    try
    {
      var game = await _gameService.GetGameByAppIdAsync(appId);

      if (game == null)
      {
        return NotFound($"Game with App ID {appId} not found");
      }

      return Ok(game);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"An error occurred while retrieving the game: {ex.Message}");
    }
  }
}
