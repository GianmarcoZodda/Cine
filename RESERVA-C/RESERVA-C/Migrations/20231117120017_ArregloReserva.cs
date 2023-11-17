using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESERVA_C.Migrations
{
    public partial class ArregloReserva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Reservas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Reservas");
        }
    }
}
