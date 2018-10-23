using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeraDAL.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroCalificaiones_Desafios_DesafioId",
                table: "RegistroCalificaiones");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroCalificaiones");

            migrationBuilder.CreateTable(
                name: "RegistroColaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColaboradorId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DesafioId = table.Column<int>(nullable: false),
                    EstudianteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroColaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistroColaboradores_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                        columns: x => new { x.CursoId, x.EstudianteId },
                        principalTable: "Rel_Cursos_Estudiantes",
                        principalColumns: new[] { "CursoId", "EstudianteId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegistroColaboradores_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                        columns: x => new { x.CursoId, x.EstudianteId, x.DesafioId },
                        principalTable: "RegistroCalificaiones",
                        principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistroColaboradores_CursoId_EstudianteId_DesafioId",
                table: "RegistroColaboradores",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroCalificaiones_Desafios_DesafioId",
                table: "RegistroCalificaiones",
                column: "DesafioId",
                principalTable: "Desafios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroCalificaiones",
                columns: new[] { "CursoId", "EstudianteId" },
                principalTable: "Rel_Cursos_Estudiantes",
                principalColumns: new[] { "CursoId", "EstudianteId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroCalificaiones_Desafios_DesafioId",
                table: "RegistroCalificaiones");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroCalificaiones");

            migrationBuilder.DropTable(
                name: "RegistroColaboradores");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroCalificaiones_Desafios_DesafioId",
                table: "RegistroCalificaiones",
                column: "DesafioId",
                principalTable: "Desafios",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroCalificaiones_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroCalificaiones",
                columns: new[] { "CursoId", "EstudianteId" },
                principalTable: "Rel_Cursos_Estudiantes",
                principalColumns: new[] { "CursoId", "EstudianteId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
