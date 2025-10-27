using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlamoraHairdresser.Data.Migrations
{
    /// <inheritdoc />
    public partial class WorkingHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "WorkingHours",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "WorkingHours");
        }
    }
}
