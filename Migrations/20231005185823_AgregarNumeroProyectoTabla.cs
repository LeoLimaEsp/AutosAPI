using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPracticaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroProyectoTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "numeroProyectos",
                columns: table => new
                {
                    ProyectoNo = table.Column<int>(type: "int", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_numeroProyectos", x => x.ProyectoNo);
                    table.ForeignKey(
                        name: "FK_numeroProyectos_Autos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Autos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 5, 12, 58, 21, 612, DateTimeKind.Local).AddTicks(5885), new DateTime(2023, 10, 5, 12, 58, 21, 612, DateTimeKind.Local).AddTicks(5904) });

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 5, 12, 58, 21, 612, DateTimeKind.Local).AddTicks(5909), new DateTime(2023, 10, 5, 12, 58, 21, 612, DateTimeKind.Local).AddTicks(5911) });

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 5, 12, 58, 21, 612, DateTimeKind.Local).AddTicks(5914), new DateTime(2023, 10, 5, 12, 58, 21, 612, DateTimeKind.Local).AddTicks(5915) });

            migrationBuilder.CreateIndex(
                name: "IX_numeroProyectos_ProyectoId",
                table: "numeroProyectos",
                column: "ProyectoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "numeroProyectos");

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5957), new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5975) });

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5981), new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5982) });

            migrationBuilder.UpdateData(
                table: "Autos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5987), new DateTime(2023, 10, 3, 13, 5, 38, 721, DateTimeKind.Local).AddTicks(5989) });
        }
    }
}
