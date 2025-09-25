using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace tmodworks.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheatCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheatCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    SteamAppId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProcessName = table.Column<string>(type: "text", nullable: false),
                    ModuleName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cheats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cheats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cheats_CheatCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CheatCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameCheats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    GameId = table.Column<string>(type: "text", nullable: false),
                    CheatId = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    BaseAddress = table.Column<string>(type: "text", nullable: false),
                    Offsets = table.Column<int[]>(type: "integer[]", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCheats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCheats_Cheats_CheatId",
                        column: x => x.CheatId,
                        principalTable: "Cheats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCheats_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CheatCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { "2d0e264d-4192-4d5a-9ed9-dc64a1ecc04b", "PLAYER" });

            migrationBuilder.InsertData(
                table: "CheatCategories",
                columns: new[] { "Id", "Name", "Priority" },
                values: new object[,]
                {
                    { "49983f5c-3382-4ead-af25-6aba0e544ac2", "ENEMIES", 3 },
                    { "6bc4e6ee-4ac3-4d83-810f-7176dd05ca49", "GAME", 5 },
                    { "a6931b4a-b337-40ed-affa-076cc42d8fa1", "TELEPORT", 7 },
                    { "ad8a1cb0-b355-4bb4-a4f4-c40b2d993943", "STATS", 2 },
                    { "b9c740f2-b8f2-48b0-aa47-9ab4728a7b25", "WEAPONS", 4 },
                    { "e3872ec1-deb7-47b7-9ec1-28a6fa00db01", "PHYSICS", 6 },
                    { "e4ad9b77-3e53-41ae-8a0f-47ea680fde27", "OTHER", 8 },
                    { "e964d84c-b235-414f-bc95-4524e761ffed", "INVENTORY", 1 }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "CreatedAt", "IsActive", "ModuleName", "Name", "ProcessName", "SteamAppId", "UpdatedAt" },
                values: new object[] { "4495544a-1d1a-44a6-87c0-f3232028e9cd", new DateTime(2025, 9, 25, 21, 47, 23, 441, DateTimeKind.Utc).AddTicks(1548), true, "Battlefront2.dll", "STAR WARS™: Battlefront Classic Collection", "Battlefront2.exe", 2446550, new DateTime(2025, 9, 25, 21, 47, 23, 441, DateTimeKind.Utc).AddTicks(1549) });

            migrationBuilder.InsertData(
                table: "Cheats",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[] { "1e15a719-b17c-4cb7-b31e-7b9eb79df3fa", "2d0e264d-4192-4d5a-9ed9-dc64a1ecc04b", new DateTime(2025, 9, 25, 21, 47, 23, 441, DateTimeKind.Utc).AddTicks(1577), true, "Health", new DateTime(2025, 9, 25, 21, 47, 23, 441, DateTimeKind.Utc).AddTicks(1578) });

            migrationBuilder.InsertData(
                table: "GameCheats",
                columns: new[] { "Id", "BaseAddress", "CheatId", "CreatedAt", "DisplayName", "GameId", "IsActive", "Offsets", "UpdatedAt" },
                values: new object[] { "5f0958e2-c7d3-43f5-aa36-1f3185185637", "023DF1B0", "1e15a719-b17c-4cb7-b31e-7b9eb79df3fa", new DateTime(2025, 9, 25, 21, 47, 23, 441, DateTimeKind.Utc).AddTicks(1612), "Infinite Health", "4495544a-1d1a-44a6-87c0-f3232028e9cd", true, new[] { 38 }, new DateTime(2025, 9, 25, 21, 47, 23, 441, DateTimeKind.Utc).AddTicks(1613) });

            migrationBuilder.CreateIndex(
                name: "IX_CheatCategories_Name",
                table: "CheatCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cheats_CategoryId",
                table: "Cheats",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheats_Name",
                table: "Cheats",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameCheats_CheatId",
                table: "GameCheats",
                column: "CheatId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCheats_GameId_CheatId",
                table: "GameCheats",
                columns: new[] { "GameId", "CheatId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_SteamAppId",
                table: "Games",
                column: "SteamAppId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCheats");

            migrationBuilder.DropTable(
                name: "Cheats");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "CheatCategories");
        }
    }
}
