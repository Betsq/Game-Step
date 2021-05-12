using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class updateSliderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "MainItemSliders");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "MainItemSliders");

            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "MainItemSliders");

            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "AdditionalItemSliders");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "AdditionalItemSliders");

            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "AdditionalItemSliders");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "MainItemSliders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "AdditionalItemSliders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainItemSliders_GameId",
                table: "MainItemSliders",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalItemSliders_GameId",
                table: "AdditionalItemSliders",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalItemSliders_Games_GameId",
                table: "AdditionalItemSliders",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MainItemSliders_Games_GameId",
                table: "MainItemSliders",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalItemSliders_Games_GameId",
                table: "AdditionalItemSliders");

            migrationBuilder.DropForeignKey(
                name: "FK_MainItemSliders_Games_GameId",
                table: "MainItemSliders");

            migrationBuilder.DropIndex(
                name: "IX_MainItemSliders_GameId",
                table: "MainItemSliders");

            migrationBuilder.DropIndex(
                name: "IX_AdditionalItemSliders_GameId",
                table: "AdditionalItemSliders");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "MainItemSliders");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "AdditionalItemSliders");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPrice",
                table: "MainItemSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "MainItemSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldPrice",
                table: "MainItemSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentPrice",
                table: "AdditionalItemSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "AdditionalItemSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldPrice",
                table: "AdditionalItemSliders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
