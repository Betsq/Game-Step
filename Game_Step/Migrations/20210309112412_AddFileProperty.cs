using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class AddFileProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameKey_Games_GameId",
                table: "GameKey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameKey",
                table: "GameKey");

            migrationBuilder.RenameTable(
                name: "GameKey",
                newName: "GameKeys");

            migrationBuilder.RenameIndex(
                name: "IX_GameKey_GameId",
                table: "GameKeys",
                newName: "IX_GameKeys_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameKeys",
                table: "GameKeys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameKeys_Games_GameId",
                table: "GameKeys",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameKeys_Games_GameId",
                table: "GameKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameKeys",
                table: "GameKeys");

            migrationBuilder.RenameTable(
                name: "GameKeys",
                newName: "GameKey");

            migrationBuilder.RenameIndex(
                name: "IX_GameKeys_GameId",
                table: "GameKey",
                newName: "IX_GameKey_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameKey",
                table: "GameKey",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameKey_Games_GameId",
                table: "GameKey",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
