using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyAPI.Migrations
{
    public partial class tablesup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPelicula",
                table: "Personajes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Personajes_IdPelicula",
                table: "Personajes",
                column: "IdPelicula");

            migrationBuilder.AddForeignKey(
                name: "FK_Personajes_Peliculas_IdPelicula",
                table: "Personajes",
                column: "IdPelicula",
                principalTable: "Peliculas",
                principalColumn: "IdPelicula",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personajes_Peliculas_IdPelicula",
                table: "Personajes");

            migrationBuilder.DropIndex(
                name: "IX_Personajes_IdPelicula",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "IdPelicula",
                table: "Personajes");
        }
    }
}
