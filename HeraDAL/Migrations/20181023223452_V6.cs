using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeraDAL.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroColaboradores_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroColaboradores");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroColaboradores_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "RegistroColaboradores");

            migrationBuilder.DropIndex(
                name: "IX_RegistroColaboradores_CursoId_EstudianteId_DesafioId",
                table: "RegistroColaboradores");

            migrationBuilder.DropColumn(
                name: "ColaboradorId",
                table: "RegistroColaboradores");

            migrationBuilder.RenameColumn(
                name: "DesafioId",
                table: "RegistroColaboradores",
                newName: "CalificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroColaboradores_CalificacionId",
                table: "RegistroColaboradores",
                column: "CalificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroColaboradores_CursoId_EstudianteId",
                table: "RegistroColaboradores",
                columns: new[] { "CursoId", "EstudianteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroColaboradores_Calificaciones_CalificacionId",
                table: "RegistroColaboradores",
                column: "CalificacionId",
                principalTable: "Calificaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroColaboradores_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroColaboradores",
                columns: new[] { "CursoId", "EstudianteId" },
                principalTable: "Rel_Cursos_Estudiantes",
                principalColumns: new[] { "CursoId", "EstudianteId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroColaboradores_Calificaciones_CalificacionId",
                table: "RegistroColaboradores");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroColaboradores_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroColaboradores");

            migrationBuilder.DropIndex(
                name: "IX_RegistroColaboradores_CalificacionId",
                table: "RegistroColaboradores");

            migrationBuilder.DropIndex(
                name: "IX_RegistroColaboradores_CursoId_EstudianteId",
                table: "RegistroColaboradores");

            migrationBuilder.RenameColumn(
                name: "CalificacionId",
                table: "RegistroColaboradores",
                newName: "DesafioId");

            migrationBuilder.AddColumn<int>(
                name: "ColaboradorId",
                table: "RegistroColaboradores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RegistroColaboradores_CursoId_EstudianteId_DesafioId",
                table: "RegistroColaboradores",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroColaboradores_Rel_Cursos_Estudiantes_CursoId_EstudianteId",
                table: "RegistroColaboradores",
                columns: new[] { "CursoId", "EstudianteId" },
                principalTable: "Rel_Cursos_Estudiantes",
                principalColumns: new[] { "CursoId", "EstudianteId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroColaboradores_RegistroCalificaiones_CursoId_EstudianteId_DesafioId",
                table: "RegistroColaboradores",
                columns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                principalTable: "RegistroCalificaiones",
                principalColumns: new[] { "CursoId", "EstudianteId", "DesafioId" },
                onDelete: ReferentialAction.SetNull);
        }
    }
}
