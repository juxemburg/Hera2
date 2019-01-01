using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HeraDAL.Migrations
{
    public partial class V11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CloneCount",
                table: "InfoSprites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CloneRemovalCount",
                table: "InfoSprites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SequentialLoopsCount",
                table: "InfoSprites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThreadCount",
                table: "InfoSprites",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CloneCount",
                table: "InfoGenerales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CloneRemovalCount",
                table: "InfoGenerales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SequentialLoopsCount",
                table: "InfoGenerales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThreadCount",
                table: "InfoGenerales",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloneCount",
                table: "InfoSprites");

            migrationBuilder.DropColumn(
                name: "CloneRemovalCount",
                table: "InfoSprites");

            migrationBuilder.DropColumn(
                name: "SequentialLoopsCount",
                table: "InfoSprites");

            migrationBuilder.DropColumn(
                name: "ThreadCount",
                table: "InfoSprites");

            migrationBuilder.DropColumn(
                name: "CloneCount",
                table: "InfoGenerales");

            migrationBuilder.DropColumn(
                name: "CloneRemovalCount",
                table: "InfoGenerales");

            migrationBuilder.DropColumn(
                name: "SequentialLoopsCount",
                table: "InfoGenerales");

            migrationBuilder.DropColumn(
                name: "ThreadCount",
                table: "InfoGenerales");
        }
    }
}
