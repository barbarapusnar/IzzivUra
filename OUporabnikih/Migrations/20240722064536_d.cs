using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OUporabnikih.Migrations
{
    /// <inheritdoc />
    public partial class d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipiId",
                table: "Registracija",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UporabnikId",
                table: "Registracija",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Registracija_TipiId",
                table: "Registracija",
                column: "TipiId");

            migrationBuilder.CreateIndex(
                name: "IX_Registracija_UporabnikId",
                table: "Registracija",
                column: "UporabnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registracija_Uporabniki_UporabnikId",
                table: "Registracija",
                column: "UporabnikId",
                principalTable: "Uporabniki",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registracija_Vrsta_TipiId",
                table: "Registracija",
                column: "TipiId",
                principalTable: "Vrsta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registracija_Uporabniki_UporabnikId",
                table: "Registracija");

            migrationBuilder.DropForeignKey(
                name: "FK_Registracija_Vrsta_TipiId",
                table: "Registracija");

            migrationBuilder.DropIndex(
                name: "IX_Registracija_TipiId",
                table: "Registracija");

            migrationBuilder.DropIndex(
                name: "IX_Registracija_UporabnikId",
                table: "Registracija");

            migrationBuilder.DropColumn(
                name: "TipiId",
                table: "Registracija");

            migrationBuilder.DropColumn(
                name: "UporabnikId",
                table: "Registracija");
        }
    }
}
