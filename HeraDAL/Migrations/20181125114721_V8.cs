using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeraDAL.Migrations
{
    public partial class V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Param1",
                table: "Desafios",
                nullable: true);
            
            migrationBuilder.AddColumn<string>(
                name: "Param2",
                table: "Desafios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Param3",
                table: "Desafios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Param4",
                table: "Desafios",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoEvaluacion",
                table: "Desafios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Param1",
                table: "Desafios");

            migrationBuilder.DropColumn(
                name: "Param2",
                table: "Desafios");

            migrationBuilder.DropColumn(
                name: "Param3",
                table: "Desafios");

            migrationBuilder.DropColumn(
                name: "Param4",
                table: "Desafios");

            migrationBuilder.DropColumn(
                name: "TipoEvaluacion",
                table: "Desafios");
        }
    }
}
