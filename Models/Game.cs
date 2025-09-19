using System.ComponentModel.DataAnnotations;

namespace Models;

public class Game
{
  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public int SteamAppId { get; set; }
  public string Name { get; set; } = string.Empty; // e.g., "Battlefront 2"
  public string ProcessName { get; set; } = string.Empty; // e.g., "Battlefront2.exe"
  public string ModuleName { get; set; } = string.Empty;  // e.g., "Battlefront2.dll"
  public bool IsActive { get; set; } = true; // If false prevent game from being included in queries
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  // Navigation properties
  public virtual ICollection<GameCheat> GameCheats { get; set; } = [];
}
