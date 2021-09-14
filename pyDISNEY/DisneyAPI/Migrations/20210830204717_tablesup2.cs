using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyAPI.Migrations
{
    public partial class tablesup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Generos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Personajes",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Peliculas",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Generos",
                type: "bytea",
                nullable: true);
        }
    }
}
