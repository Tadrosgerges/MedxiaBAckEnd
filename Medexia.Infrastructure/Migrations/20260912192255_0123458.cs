using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medexia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _0123458 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "TimeTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_ClinicId",
                table: "TimeTables",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Clinic_ClinicId",
                table: "TimeTables",
                column: "ClinicId",
                principalTable: "Clinic",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Clinic_ClinicId",
                table: "TimeTables");

            migrationBuilder.DropTable(
                name: "Clinic");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_ClinicId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "TimeTables");
        }
    }
}
