using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecommendedSystemRequirements_GameId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_MinimumSystemRequirements_GameId",
                table: "MinimumSystemRequirements");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedSystemRequirements_GameId",
                table: "RecommendedSystemRequirements",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirements_GameId",
                table: "MinimumSystemRequirements",
                column: "GameId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecommendedSystemRequirements_GameId",
                table: "RecommendedSystemRequirements");

            migrationBuilder.DropIndex(
                name: "IX_MinimumSystemRequirements_GameId",
                table: "MinimumSystemRequirements");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedSystemRequirements_GameId",
                table: "RecommendedSystemRequirements",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_MinimumSystemRequirements_GameId",
                table: "MinimumSystemRequirements",
                column: "GameId");
        }
    }
}
