using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessWebApi.Migrations
{
    /// <inheritdoc />
    public partial class opened_new_file : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "blackName",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "whiteName",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blackName",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "whiteName",
                table: "Games");
        }
    }
}
