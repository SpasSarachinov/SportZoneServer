using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportZoneServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init22132 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountPercantage",
                table: "Products",
                newName: "DiscountPercentage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "Products",
                newName: "DiscountPercantage");
        }
    }
}
