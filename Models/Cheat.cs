using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class Cheat
{
  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();

  public string Name { get; set; } = string.Empty;

  [ForeignKey(nameof(Category))]
  public string CategoryId { get; set; } = string.Empty; // e.g., "PLAYER", "INVENTORY", "STATS", "ENEMIES", "WEAPONS", "GAME", "PHYSICS", "TELEPORT", "OTHER"

  public bool IsActive { get; set; } = true; // If false prevent cheat from being included in queries

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

  // Navigation properties
  public virtual CheatCategory Category { get; set; } = null!;
  public virtual ICollection<GameCheat> GameCheats { get; set; } = [];
}
