using Microsoft.EntityFrameworkCore.Migrations;

namespace Amazon.Scrapper.EF.Migrations
{
    public partial class Remove_Price_From_Product_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Product",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
