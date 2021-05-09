using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class remove2propertyWithID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderNumbers_OrderNumberId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderKeysGameId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderNumberId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderNumbers_OrderNumberId",
                table: "Orders",
                column: "OrderNumberId",
                principalTable: "OrderNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderNumbers_OrderNumberId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderNumberId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderKeysGameId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderNumbers_OrderNumberId",
                table: "Orders",
                column: "OrderNumberId",
                principalTable: "OrderNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
