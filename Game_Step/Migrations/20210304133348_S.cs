using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class S : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameMinimumSystemRequirements");

            migrationBuilder.DropTable(
                name: "GameRecommendedSystemRequirements");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "RecommendedSystemRequirements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "MinimumSystemRequirements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedSystemRequirements_GameId",
                table: "RecommendedSystemRequirements",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirements_GameId",
                table: "MinimumSystemRequirements",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinimumSystemRequirements_Games_GameId",
                table: "MinimumSystemRequirements",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedSystemRequirements_Games_GameId",
                table: "RecommendedSystemRequirements",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinimumSystemRequirements_Games_GameId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedSystemRequirements_Games_GameId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_RecommendedSystemRequirements_GameId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_MinimumSystemRequirements_GameId",
                table: "MinimumSystemRequirements");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "MinimumSystemRequirements");

            migrationBuilder.CreateTable(
                name: "GameMinimumSystemRequirements",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    MinSysReqId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMinimumSystemRequirements", x => new { x.GamesId, x.MinSysReqId });
                    table.ForeignKey(
                        name: "FK_GameMinimumSystemRequirements_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameMinimumSystemRequirements_MinimumSystemRequirements_MinSysReqId",
                        column: x => x.MinSysReqId,
                        principalTable: "MinimumSystemRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameRecommendedSystemRequirements",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    RecSysReqId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRecommendedSystemRequirements", x => new { x.GamesId, x.RecSysReqId });
                    table.ForeignKey(
                        name: "FK_GameRecommendedSystemRequirements_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameRecommendedSystemRequirements_RecommendedSystemRequirements_RecSysReqId",
                        column: x => x.RecSysReqId,
                        principalTable: "RecommendedSystemRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameMinimumSystemRequirements_MinSysReqId",
                table: "GameMinimumSystemRequirements",
                column: "MinSysReqId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRecommendedSystemRequirements_RecSysReqId",
                table: "GameRecommendedSystemRequirements",
                column: "RecSysReqId");
        }
    }
}
