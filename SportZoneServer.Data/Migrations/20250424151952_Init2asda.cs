using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportZoneServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init2asda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURI",
                table: "Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURI",
                table: "Categories");
        }
    }
}
