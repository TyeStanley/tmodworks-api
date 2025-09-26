using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class GameCheat
{
  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();
  [ForeignKey(nameof(Game))]
  public string GameId { get; set; } = string.Empty;
  [ForeignKey(nameof(Cheat))]
  public string CheatId { get; set; } = string.Empty;
  public string? DisplayName { get; set; } // e.g., "Max Health, "Max Ammo", "Max Speed", etc.
  public string BaseAddress { get; set; } = string.Empty; // e.g., "023DF1B0"
  public int[] Offsets { get; set; } = []; // e.g., [38]
  public bool IsActive { get; set; } = true; // If false prevent game cheat from being included in queries
  public string ControlType { get; set; } = "SLIDER"; // Control type for frontend (SLIDER, TOGGLE, STEPPER)
  public decimal? Min { get; set; } // Minimum value for the control
  public decimal? Max { get; set; } // Maximum value for the control
  public decimal? Step { get; set; } // Step value for the control
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  // Navigation properties
  public virtual Game Game { get; set; } = null!;
  public virtual Cheat Cheat { get; set; } = null!;
}
