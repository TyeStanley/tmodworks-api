using System.ComponentModel.DataAnnotations;

namespace Models;

public class CheatCategory
{
  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public CheatCategoryType Name { get; set; } // e.g., "PLAYER", "INVENTORY", "STATS", "ENEMIES", "WEAPONS", "GAME", "PHYSICS", "TELEPORT", "OTHER"
  public int Priority { get; set; } = 0; // e.g., 0, 1, 2, 3, 4, 5, 6, 7, 8
  // Navigation properties
  public virtual ICollection<Cheat> Cheats { get; set; } = [];
}
