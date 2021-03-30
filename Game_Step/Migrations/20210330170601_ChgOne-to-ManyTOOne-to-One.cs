using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class ChgOnetoManyTOOnetoOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GamePrices_GameId",
                table: "GamePrices");

            migrationBuilder.CreateIndex(
                name: "IX_GamePrices_GameId",
                table: "GamePrices",
                column: "GameId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GamePrices_GameId",
                table: "GamePrices");

            migrationBuilder.CreateIndex(
                name: "IX_GamePrices_GameId",
                table: "GamePrices",
                column: "GameId");
        }
    }
}
