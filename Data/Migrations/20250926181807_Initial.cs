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
                    ControlType = table.Column<string>(type: "text", nullable: false, defaultValue: "TOGGLE"),
                    Min = table.Column<decimal>(type: "numeric(18,6)", nullable: true),
                    Max = table.Column<decimal>(type: "numeric(18,6)", nullable: true),
                    Step = table.Column<decimal>(type: "numeric(18,6)", nullable: true),
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
                columns: new[] { "Id", "Name", "Priority" },
                values: new object[,]
                {
                    { "0f5afa3f-3d99-4969-91f6-c75d867269c9", "OTHER", 8 },
                    { "4699a053-420d-4ecd-aac5-eba93ed4167a", "TELEPORT", 7 },
                    { "62cc82e6-6688-48a6-8fe5-a0149e8208e8", "PHYSICS", 6 },
                    { "74bab71c-6522-4b58-9e02-f3c72108a2cb", "INVENTORY", 1 },
                    { "86c8bd7f-d407-460a-a6e4-29903fbbc5be", "WEAPONS", 4 }
                });

            migrationBuilder.InsertData(
                table: "CheatCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { "a48f0eb2-f930-48fa-8f6d-fd6f265fca71", "PLAYER" });

            migrationBuilder.InsertData(
                table: "CheatCategories",
                columns: new[] { "Id", "Name", "Priority" },
                values: new object[,]
                {
                    { "bbcc9473-2b20-4675-a951-6bfb04ef17c6", "GAME", 5 },
                    { "c9540210-a1ef-425f-a048-1e0170d1d175", "STATS", 2 },
                    { "ef0e46f8-2003-4f90-93b7-30c614160dd1", "ENEMIES", 3 }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "CreatedAt", "IsActive", "ModuleName", "Name", "ProcessName", "SteamAppId", "UpdatedAt" },
                values: new object[] { "0ecd7690-b369-461e-8a99-4ba1f3a0c457", new DateTime(2025, 9, 26, 18, 18, 6, 838, DateTimeKind.Utc).AddTicks(8543), true, "Battlefront2.dll", "STAR WARS™: Battlefront Classic Collection", "Battlefront2.exe", 2446550, new DateTime(2025, 9, 26, 18, 18, 6, 838, DateTimeKind.Utc).AddTicks(8543) });

            migrationBuilder.InsertData(
                table: "Cheats",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[] { "4fd1cd7e-c97b-42ba-b604-6b7aa557e527", "a48f0eb2-f930-48fa-8f6d-fd6f265fca71", new DateTime(2025, 9, 26, 18, 18, 6, 838, DateTimeKind.Utc).AddTicks(8571), true, "Health", new DateTime(2025, 9, 26, 18, 18, 6, 838, DateTimeKind.Utc).AddTicks(8572) });

            migrationBuilder.InsertData(
                table: "GameCheats",
                columns: new[] { "Id", "BaseAddress", "CheatId", "ControlType", "CreatedAt", "DisplayName", "GameId", "IsActive", "Max", "Min", "Offsets", "Step", "UpdatedAt" },
                values: new object[] { "a6f97787-2192-4df4-86f7-e7525574bbd0", "023DF1B0", "4fd1cd7e-c97b-42ba-b604-6b7aa557e527", "TOGGLE", new DateTime(2025, 9, 26, 18, 18, 6, 838, DateTimeKind.Utc).AddTicks(8603), "Infinite Health", "0ecd7690-b369-461e-8a99-4ba1f3a0c457", true, null, null, new[] { 38 }, null, new DateTime(2025, 9, 26, 18, 18, 6, 838, DateTimeKind.Utc).AddTicks(8604) });

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
