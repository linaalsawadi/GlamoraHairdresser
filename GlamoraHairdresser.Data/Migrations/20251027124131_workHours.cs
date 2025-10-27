using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlamoraHairdresser.Data.Migrations
{
    /// <inheritdoc />
    public partial class workHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DayOfWeek",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.CreateTable(
                name: "SpecialWorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    OpenTime = table.Column<TimeOnly>(type: "time(0)", nullable: true),
                    CloseTime = table.Column<TimeOnly>(type: "time(0)", nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialWorkingHours", x => x.Id);
                    table.CheckConstraint("CK_SWH_Time", "([IsClosed] = 1) OR ([OpenTime] IS NOT NULL AND [CloseTime] IS NOT NULL AND [OpenTime] < [CloseTime])");
                    table.ForeignKey(
                        name: "FK_SpecialWorkingHours_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerSpecialWorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    OpenTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    CloseTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    IsOffDay = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSpecialWorkingHours", x => x.Id);
                    table.CheckConstraint("CK_WorkerSpecialWorkingHour_Times", "([IsOffDay] = 1) OR ([OpenTime] < [CloseTime])");
                    table.ForeignKey(
                        name: "FK_WorkerSpecialWorkingHours_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerWorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    OpenTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerWorkingHours", x => x.Id);
                    table.CheckConstraint("CK_WorkerWorkingHour_Times", "[OpenTime] < [CloseTime]");
                    table.ForeignKey(
                        name: "FK_WorkerWorkingHours_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialWorkingHours_SalonId_Date",
                table: "SpecialWorkingHours",
                columns: new[] { "SalonId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSpecialWorkingHours_WorkerId_Date",
                table: "WorkerSpecialWorkingHours",
                columns: new[] { "WorkerId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerWorkingHours_WorkerId_DayOfWeek",
                table: "WorkerWorkingHours",
                columns: new[] { "WorkerId", "DayOfWeek" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialWorkingHours");

            migrationBuilder.DropTable(
                name: "WorkerSpecialWorkingHours");

            migrationBuilder.DropTable(
                name: "WorkerWorkingHours");

            migrationBuilder.AlterColumn<byte>(
                name: "DayOfWeek",
                table: "WorkingHours",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
