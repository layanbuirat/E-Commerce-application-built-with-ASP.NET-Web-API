using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SHOP.DAL.Data.Migrations
{
    public partial class addBrandImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Brands");
        }
    }
}
