using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelNaServerju.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kolo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Znamka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLastnika = table.Column<int>(type: "int", nullable: false),
                    LastnikId = table.Column<int>(type: "int", nullable: false),
                    TrentnaLokacijaLongitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrentnaLokacijaLatitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kolo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kolo_Uporabniki_LastnikId",
                        column: x => x.LastnikId,
                        principalTable: "Uporabniki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kolo_LastnikId",
                table: "Kolo",
                column: "LastnikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kolo");
        }
    }
}
