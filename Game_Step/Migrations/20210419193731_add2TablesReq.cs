
using Microsoft.EntityFrameworkCore.Migrations;

namespace Game_Step.Migrations
{
    public partial class add2TablesReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumCPU",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MinimumDirectX",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MinimumHDD",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MinimumOC",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MinimumRAM",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "MinimumVideoCard",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendCPU",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendDirectX",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendHDD",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendOC",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendRAM",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "RecommendVideoCard",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "isCommentChecked",
                table: "SubComments",
                newName: "IsCommentChecked");

            migrationBuilder.RenameColumn(
                name: "isCommentChecked",
                table: "MainComments",
                newName: "IsCommentChecked");

            migrationBuilder.CreateTable(
                name: "GameMinimums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinimumOC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumCPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumRAM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumVideoCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumDirectX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumHDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameMinimums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameMinimums_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecommendOC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendCPU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendRAM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendVideoCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendDirectX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecommendHDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRecommendations_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameMinimums_GameId",
                table: "GameMinimums",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRecommendations_GameId",
                table: "GameRecommendations",
                column: "GameId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameMinimums");

            migrationBuilder.DropTable(
                name: "GameRecommendations");

            migrationBuilder.RenameColumn(
                name: "IsCommentChecked",
                table: "SubComments",
                newName: "isCommentChecked");

            migrationBuilder.RenameColumn(
                name: "IsCommentChecked",
                table: "MainComments",
                newName: "isCommentChecked");

            migrationBuilder.AddColumn<string>(
                name: "MinimumCPU",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimumDirectX",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimumHDD",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimumOC",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimumRAM",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MinimumVideoCard",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendCPU",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendDirectX",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendHDD",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendOC",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendRAM",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecommendVideoCard",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
