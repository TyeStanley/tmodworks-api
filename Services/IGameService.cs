using Models;

namespace Services;

public interface IGameService
{
  Task<IEnumerable<Game>> GetAllGamesAsync(string? search = null);
  Task<Game?> GetGameByAppIdAsync(int appId);
}
