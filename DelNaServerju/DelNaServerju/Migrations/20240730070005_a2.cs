using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelNaServerju.Migrations
{
    /// <inheritdoc />
    public partial class a2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdLastnika",
                table: "Kolo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdLastnika",
                table: "Kolo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
