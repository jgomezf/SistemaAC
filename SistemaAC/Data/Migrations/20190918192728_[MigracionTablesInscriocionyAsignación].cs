using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SistemaAC.Data.Migrations
{
    public partial class MigracionTablesInscriocionyAsignación : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Asignacion",
                columns: table => new
                {
                    AsignacioniD = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CursoID = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    InstructorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignacion", x => x.AsignacioniD);
                });

            migrationBuilder.CreateTable(
                name: "Inscripcion",
                columns: table => new
                {
                    InscripcionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CursoID = table.Column<int>(nullable: false),
                    EstudienateID = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Grado = table.Column<int>(nullable: false),
                    Pago = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripcion", x => x.InscripcionID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignacion");

            migrationBuilder.DropTable(
                name: "Inscripcion");
        }
    }
}
