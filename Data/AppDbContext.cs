using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<Game> Games { get; set; }
  public DbSet<Cheat> Cheats { get; set; }
  public DbSet<GameCheat> GameCheats { get; set; }
  public DbSet<CheatCategory> CheatCategories { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Configure Game entity
    modelBuilder.Entity<Game>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
      entity.HasIndex(e => e.SteamAppId).IsUnique();
      entity.Property(e => e.Name).IsRequired();
      entity.Property(e => e.ProcessName).IsRequired();
      entity.Property(e => e.ModuleName).IsRequired();
      entity.Property(e => e.IsActive).HasDefaultValue(true);
      entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
      entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    });

    // Configure CheatCategory entity
    modelBuilder.Entity<CheatCategory>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
      entity.HasIndex(e => e.Name).IsUnique();
      entity.Property(e => e.Name).HasConversion<string>();
      entity.Property(e => e.Priority).HasDefaultValue(0);
    });

    // Configure Cheat entity
    modelBuilder.Entity<Cheat>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
      entity.HasIndex(e => e.Name).IsUnique();
      entity.Property(e => e.Name).IsRequired();
      entity.Property(e => e.CategoryId).IsRequired();
      entity.Property(e => e.IsActive).HasDefaultValue(true);
      entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
      entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

      // Configure relationship with CheatCategory
      entity.HasOne(e => e.Category)
                .WithMany(c => c.Cheats)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
    });

    // Configure GameCheat entity
    modelBuilder.Entity<GameCheat>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
      entity.Property(e => e.GameId).IsRequired();
      entity.Property(e => e.CheatId).IsRequired();
      entity.Property(e => e.DisplayName).IsRequired();
      entity.Property(e => e.BaseAddress).IsRequired();
      entity.Property(e => e.Offsets).HasColumnType("integer[]");
      entity.Property(e => e.IsActive).HasDefaultValue(true);
      entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
      entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

      // Configure unique constraint for GameId and CheatId combination
      entity.HasIndex(e => new { e.GameId, e.CheatId }).IsUnique();

      // Configure relationships
      entity.HasOne(e => e.Game)
                .WithMany(g => g.GameCheats)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);

      entity.HasOne(e => e.Cheat)
                .WithMany(c => c.GameCheats)
                .HasForeignKey(e => e.CheatId)
                .OnDelete(DeleteBehavior.Cascade);
    });

    // Seed initial data for CheatCategory
    modelBuilder.Entity<CheatCategory>().HasData(
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.PLAYER, Priority = 0 },
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.INVENTORY, Priority = 1 },
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.STATS, Priority = 2 },
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.ENEMIES, Priority = 3 },
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.WEAPONS, Priority = 4 },
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.GAME, Priority = 5 },
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.PHYSICS, Priority = 6 },
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.TELEPORT, Priority = 7 },
        new CheatCategory { Id = Guid.NewGuid().ToString(), Name = CheatCategoryType.OTHER, Priority = 8 }
    );

    // Seed initial data for Game
    var battlefrontGameId = "11111111-1111-1111-1111-111111111111";
    var healthCheatId = "22222222-2222-2222-2222-222222222222";

    modelBuilder.Entity<Game>().HasData(
        new Game
        {
          Id = battlefrontGameId,
          SteamAppId = 2446550,
          Name = "STAR WARS™: Battlefront Classic Collection",
          ProcessName = "Battlefront2.exe",
          ModuleName = "Battlefront2.dll",
          IsActive = true,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        }
    );

    // Seed initial data for Cheat
    modelBuilder.Entity<Cheat>().HasData(
        new Cheat
        {
          Id = healthCheatId,
          Name = "Health",
          CategoryId = "PLAYER",
          IsActive = true,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        }
    );

    // Seed initial data for GameCheat
    modelBuilder.Entity<GameCheat>().HasData(
        new GameCheat
        {
          Id = Guid.NewGuid().ToString(),
          GameId = battlefrontGameId,
          CheatId = healthCheatId,
          DisplayName = "Infinite Health",
          BaseAddress = "023DF1B0",
          Offsets = [38],
          IsActive = true,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        }
    );
  }
}
