using Microsoft.EntityFrameworkCore.Migrations;

namespace DisneyAPI.Migrations
{
    public partial class tablesup3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_Personajes_IdPersonaje",
                table: "Peliculas");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_IdPersonaje",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "IdPersonaje",
                table: "Peliculas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPersonaje",
                table: "Peliculas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_IdPersonaje",
                table: "Peliculas",
                column: "IdPersonaje");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_Personajes_IdPersonaje",
                table: "Peliculas",
                column: "IdPersonaje",
                principalTable: "Personajes",
                principalColumn: "IdPersonaje",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
