using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class onetomanyorders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNumberId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumberId",
                table: "Orders",
                column: "OrderNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderNumbers_OrderNumberId",
                table: "Orders",
                column: "OrderNumberId",
                principalTable: "OrderNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderNumbers_OrderNumberId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderNumberId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderNumberId",
                table: "Orders");
        }
    }
}
