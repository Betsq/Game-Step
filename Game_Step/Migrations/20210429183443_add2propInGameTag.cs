using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class add2propInGameTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GameTags");

            migrationBuilder.DropColumn(
                name: "NameParam",
                table: "GameTags");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "GameTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GameTags_TagId",
                table: "GameTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTags_Tags_TagId",
                table: "GameTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTags_Tags_TagId",
                table: "GameTags");

            migrationBuilder.DropIndex(
                name: "IX_GameTags_TagId",
                table: "GameTags");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "GameTags");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GameTags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameParam",
                table: "GameTags",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
