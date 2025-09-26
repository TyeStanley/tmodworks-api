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
      entity.Property(e => e.ControlType).IsRequired().HasDefaultValue("TOGGLE");
      entity.Property(e => e.Min).HasColumnType("decimal(18,6)");
      entity.Property(e => e.Max).HasColumnType("decimal(18,6)");
      entity.Property(e => e.Step).HasColumnType("decimal(18,6)");
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

    // Seed initial data
    Seeding.SeedData(modelBuilder);
  }
}
