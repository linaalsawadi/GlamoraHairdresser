using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlamoraHairdresser.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCheckConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkingHour_Time",
                table: "WorkingHours");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkingHour_Time",
                table: "WorkingHours",
                sql: "([IsOpen] = 0) OR ([OpenTime] < [CloseTime])");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkingHour_Time",
                table: "WorkingHours");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkingHour_Time",
                table: "WorkingHours",
                sql: "[OpenTime] < [CloseTime]");
        }
    }
}
