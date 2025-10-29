using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlamoraHairdresser.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixWorkerHourCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkerWorkingHour_Times",
                table: "WorkerWorkingHours");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkerWorkingHour_Day",
                table: "WorkerWorkingHours",
                sql: "[DayOfWeek] BETWEEN 0 AND 6");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkerWorkingHour_Times",
                table: "WorkerWorkingHours",
                sql: "([IsOpen] = 0 AND [OpenTime] = [CloseTime]) OR ([IsOpen] = 1 AND [OpenTime] < [CloseTime])");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkerWorkingHour_Day",
                table: "WorkerWorkingHours");

            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkerWorkingHour_Times",
                table: "WorkerWorkingHours");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkerWorkingHour_Times",
                table: "WorkerWorkingHours",
                sql: "[OpenTime] < [CloseTime]");
        }
    }
}
