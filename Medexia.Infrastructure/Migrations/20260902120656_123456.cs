using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medexia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _123456 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isItRated",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isItRated",
                table: "Appointments");
        }
    }
}
