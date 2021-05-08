using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class addPropertyIsAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "SubComments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "MainComments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "MainComments");
        }
    }
}
