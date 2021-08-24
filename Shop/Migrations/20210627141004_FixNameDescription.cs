using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class FixNameDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desctiption",
                table: "Products",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Desctiption");
        }
    }
}
