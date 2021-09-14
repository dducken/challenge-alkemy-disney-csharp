using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyAPI.Migrations
{
    public partial class tablesup4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personajes_Peliculas_IdPelicula",
                table: "Personajes");

            migrationBuilder.RenameColumn(
                name: "IdPelicula",
                table: "Personajes",
                newName: "idPelicula");

            migrationBuilder.RenameIndex(
                name: "IX_Personajes_IdPelicula",
                table: "Personajes",
                newName: "IX_Personajes_idPelicula");

            migrationBuilder.AddForeignKey(
                name: "FK_Personajes_Peliculas_idPelicula",
                table: "Personajes",
                column: "idPelicula",
                principalTable: "Peliculas",
                principalColumn: "IdPelicula",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personajes_Peliculas_idPelicula",
                table: "Personajes");

            migrationBuilder.RenameColumn(
                name: "idPelicula",
                table: "Personajes",
                newName: "IdPelicula");

            migrationBuilder.RenameIndex(
                name: "IX_Personajes_idPelicula",
                table: "Personajes",
                newName: "IX_Personajes_IdPelicula");

            migrationBuilder.AddForeignKey(
                name: "FK_Personajes_Peliculas_IdPelicula",
                table: "Personajes",
                column: "IdPelicula",
                principalTable: "Peliculas",
                principalColumn: "IdPelicula",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
