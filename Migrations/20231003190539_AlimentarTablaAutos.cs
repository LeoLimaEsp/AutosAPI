using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoPracticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaAutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Autos",
                columns: new[] { "Id", "FechaActualizacion", "FechaCreacion", "Hp", "ImagenUrl", "Marca", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5957), new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5975), 135, "", "Volkswagen", "Tiguan", 539000 },
                    { 2, new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5981), new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5982), 500, "", "Cupra", "Ateca", 959000 },
                    { 3, new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5987), new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5989), 525, "", "Porsche", "911 GT3-RS", 4778000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
