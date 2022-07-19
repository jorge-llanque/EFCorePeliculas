using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCorePeliculas.Migrations
{
    public partial class addborrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EstaBorrado",
                table: "generos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "cineOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "cineOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 7, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstaBorrado",
                table: "generos");

            migrationBuilder.UpdateData(
                table: "cineOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 7, 13, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "cineOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFin", "FechaInicio" },
                values: new object[] { new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 7, 13, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
