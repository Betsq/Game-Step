using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class AddSomeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OrderNumbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "OrderNumbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Promocode",
                table: "OrderNumbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "OrderNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "UserAgreement",
                table: "OrderNumbers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "OrderNumbers");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "OrderNumbers");

            migrationBuilder.DropColumn(
                name: "Promocode",
                table: "OrderNumbers");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "OrderNumbers");

            migrationBuilder.DropColumn(
                name: "UserAgreement",
                table: "OrderNumbers");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
