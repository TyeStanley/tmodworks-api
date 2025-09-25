using Microsoft.EntityFrameworkCore;
using Models;
using Data;

namespace Services;

public class GameService(AppDbContext context) : IGameService
{
  private readonly AppDbContext _context = context;

  // Get all active games (basic info only) with optional search
  public async Task<IEnumerable<Game>> GetAllGamesAsync(string? search = null)
  {
    var query = _context.Games.Where(g => g.IsActive);

    if (!string.IsNullOrWhiteSpace(search))
    {
      query = query.Where(g => EF.Functions.ILike(g.Name, $"%{search}%"));
    }

    var results = await query
        .OrderBy(g => g.Name)
        .ToListAsync();

    return results;
  }

  public async Task<Game?> GetGameByAppIdAsync(int appId)
  {
    return await _context.Games
        .Where(g => g.SteamAppId == appId && g.IsActive)
        .Include(g => g.GameCheats.OrderBy(gc => gc.Cheat.Category.Priority)
            .ThenBy(gc => gc.Cheat.Category.Name)
            .ThenBy(gc => gc.DisplayName))
            .ThenInclude(gc => gc.Cheat)
                .ThenInclude(c => c.Category)
        .FirstOrDefaultAsync();
  }
}
