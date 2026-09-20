using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medexia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _012345679 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "Clinic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clinic_DoctorID",
                table: "Clinic",
                column: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clinic_Doctors_DoctorID",
                table: "Clinic",
                column: "DoctorID",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clinic_Doctors_DoctorID",
                table: "Clinic");

            migrationBuilder.DropIndex(
                name: "IX_Clinic_DoctorID",
                table: "Clinic");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "Clinic");
        }
    }
}
