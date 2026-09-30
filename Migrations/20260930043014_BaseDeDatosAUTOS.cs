using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAutos.Migrations
{
    /// <inheritdoc />
    public partial class BaseDeDatosAUTOS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AutoId",
                table: "CargaAuto",
                newName: "AutosId");

            migrationBuilder.AlterColumn<bool>(
                name: "Disponible",
                table: "CargaAuto",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AutosId",
                table: "CargaAuto",
                newName: "AutoId");

            migrationBuilder.AlterColumn<bool>(
                name: "Disponible",
                table: "CargaAuto",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
