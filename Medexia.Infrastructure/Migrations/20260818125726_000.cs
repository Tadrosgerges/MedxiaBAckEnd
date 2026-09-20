using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medexia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPatients",
                table: "TimeTables",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPatients",
                table: "TimeTables");
        }
    }
}
