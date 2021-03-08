using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class CreateQuantityOfGoodsProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityOfGoods",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityOfGoods",
                table: "Games");
        }
    }
}
