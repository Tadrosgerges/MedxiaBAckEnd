using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medexia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tsts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Doctors_DoctorId",
                table: "Rates");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Rates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Doctors_DoctorId",
                table: "Rates",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Doctors_DoctorId",
                table: "Rates");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Rates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Doctors_DoctorId",
                table: "Rates",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }
    }
}
