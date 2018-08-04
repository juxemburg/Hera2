using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeraDAL.Migrations
{
    public partial class V_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Tiempoinicio",
                table: "Calificaciones",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Tiempoinicio",
                table: "Calificaciones",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
