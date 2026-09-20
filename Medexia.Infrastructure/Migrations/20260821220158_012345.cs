using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medexia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _012345 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dateOftheQueue",
                table: "QueueItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateOftheQueue",
                table: "QueueItems");
        }
    }
}
