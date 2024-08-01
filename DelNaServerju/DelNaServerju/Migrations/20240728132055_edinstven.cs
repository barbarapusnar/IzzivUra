using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelNaServerju.Migrations
{
    /// <inheritdoc />
    public partial class edinstven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "Uporabniki",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Uporabniki_Ime",
                table: "Uporabniki",
                column: "Ime",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Uporabniki_Ime",
                table: "Uporabniki");

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "Uporabniki",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
