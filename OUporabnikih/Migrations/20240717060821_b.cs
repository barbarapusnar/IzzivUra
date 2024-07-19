using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OUporabnikih.Migrations
{
    /// <inheritdoc />
    public partial class b : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Todos",
                table: "Todos");

            migrationBuilder.RenameTable(
                name: "Todos",
                newName: "Uporabniki");

            migrationBuilder.RenameIndex(
                name: "IX_Todos_Ime",
                table: "Uporabniki",
                newName: "IX_Uporabniki_Ime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Uporabniki",
                table: "Uporabniki",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Uporabniki",
                table: "Uporabniki");

            migrationBuilder.RenameTable(
                name: "Uporabniki",
                newName: "Todos");

            migrationBuilder.RenameIndex(
                name: "IX_Uporabniki_Ime",
                table: "Todos",
                newName: "IX_Todos_Ime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todos",
                table: "Todos",
                column: "Id");
        }
    }
}
