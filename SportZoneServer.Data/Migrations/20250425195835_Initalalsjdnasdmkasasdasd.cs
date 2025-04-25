using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportZoneServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initalalsjdnasdmkasasdasd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PriceBeforeDiscount",
                table: "Orders",
                newName: "OrderTotalPrice");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryImageUri",
                table: "OrderItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "OrderItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryImageUri",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "OrderTotalPrice",
                table: "Orders",
                newName: "PriceBeforeDiscount");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                table: "Orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
