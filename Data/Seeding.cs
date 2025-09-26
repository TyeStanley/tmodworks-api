using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public static class Seeding
{
  // Generate random UUIDs but keep them consistent within the seeding process
  private static readonly string PlayerCategoryId = Guid.NewGuid().ToString();
  private static readonly string InventoryCategoryId = Guid.NewGuid().ToString();
  private static readonly string StatsCategoryId = Guid.NewGuid().ToString();
  private static readonly string EnemiesCategoryId = Guid.NewGuid().ToString();
  private static readonly string WeaponsCategoryId = Guid.NewGuid().ToString();
  private static readonly string GameCategoryId = Guid.NewGuid().ToString();
  private static readonly string PhysicsCategoryId = Guid.NewGuid().ToString();
  private static readonly string TeleportCategoryId = Guid.NewGuid().ToString();
  private static readonly string OtherCategoryId = Guid.NewGuid().ToString();

  private static readonly string BattlefrontGameId = Guid.NewGuid().ToString();
  private static readonly string HealthCheatId = Guid.NewGuid().ToString();

  public static void SeedData(ModelBuilder modelBuilder)
  {
    SeedCheatCategories(modelBuilder);
    SeedGames(modelBuilder);
    SeedCheats(modelBuilder);
    SeedGameCheats(modelBuilder);
  }

  private static void SeedCheatCategories(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<CheatCategory>().HasData(
        new CheatCategory { Id = PlayerCategoryId, Name = CheatCategoryType.PLAYER, Priority = 0 },
        new CheatCategory { Id = InventoryCategoryId, Name = CheatCategoryType.INVENTORY, Priority = 1 },
        new CheatCategory { Id = StatsCategoryId, Name = CheatCategoryType.STATS, Priority = 2 },
        new CheatCategory { Id = EnemiesCategoryId, Name = CheatCategoryType.ENEMIES, Priority = 3 },
        new CheatCategory { Id = WeaponsCategoryId, Name = CheatCategoryType.WEAPONS, Priority = 4 },
        new CheatCategory { Id = GameCategoryId, Name = CheatCategoryType.GAME, Priority = 5 },
        new CheatCategory { Id = PhysicsCategoryId, Name = CheatCategoryType.PHYSICS, Priority = 6 },
        new CheatCategory { Id = TeleportCategoryId, Name = CheatCategoryType.TELEPORT, Priority = 7 },
        new CheatCategory { Id = OtherCategoryId, Name = CheatCategoryType.OTHER, Priority = 8 }
    );
  }

  private static void SeedGames(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Game>().HasData(
        new Game
        {
          Id = BattlefrontGameId,
          SteamAppId = 2446550,
          Name = "STAR WARS™: Battlefront Classic Collection",
          ProcessName = "Battlefront2.exe",
          ModuleName = "Battlefront2.dll",
          IsActive = true,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        }
    );
  }

  private static void SeedCheats(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Cheat>().HasData(
        new Cheat
        {
          Id = HealthCheatId,
          Name = "Health",
          CategoryId = PlayerCategoryId,
          IsActive = true,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        }
    );
  }

  private static void SeedGameCheats(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<GameCheat>().HasData(
        new GameCheat
        {
          Id = Guid.NewGuid().ToString(),
          GameId = BattlefrontGameId,
          CheatId = HealthCheatId,
          DisplayName = "Infinite Health",
          BaseAddress = "023DF1B0",
          Offsets = [38],
          IsActive = true,
          ControlType = "TOGGLE",
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        }
    );
  }
}
