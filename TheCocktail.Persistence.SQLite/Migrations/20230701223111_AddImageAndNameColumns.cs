using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheCocktail.Persistence.SQLite.Migrations
{
    public partial class AddImageAndNameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DrinkImage",
                table: "Favorites",
                type: "NVARCHAR",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DrinkName",
                table: "Favorites",
                type: "NVARCHAR",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrinkImage",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "DrinkName",
                table: "Favorites");
        }
    }
}
