using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlamoraHairdresser.Data.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingHours_SalonId",
                table: "WorkingHours");

            migrationBuilder.DropIndex(
                name: "IX_ServiceOfferings_SalonId",
                table: "ServiceOfferings");

            migrationBuilder.AlterColumn<byte>(
                name: "DayOfWeek",
                table: "WorkingHours",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ServiceOfferings",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceOfferings",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Salons",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Salons",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Salons",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalonId = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.CheckConstraint("CK_UserType_NotEmpty", "[UserType] IN ('Admin','Worker','Customer')");
                    table.ForeignKey(
                        name: "FK_Users_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonId = table.Column<int>(type: "int", nullable: false),
                    ServiceOfferingId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    StartUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndUtc = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    PriceAtBooking = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    DurationMinutes = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.CheckConstraint("CK_Appt_Time", "[StartUtc] < [EndUtc]");
                    table.ForeignKey(
                        name: "FK_Appointments_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_ServiceOfferings_ServiceOfferingId",
                        column: x => x.ServiceOfferingId,
                        principalTable: "ServiceOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSkills",
                columns: table => new
                {
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    ServiceOfferingId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSkills", x => new { x.WorkerId, x.ServiceOfferingId });
                    table.ForeignKey(
                        name: "FK_EmployeeSkills_ServiceOfferings_ServiceOfferingId",
                        column: x => x.ServiceOfferingId,
                        principalTable: "ServiceOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSkills_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkerAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Start = table.Column<TimeOnly>(type: "time(0)", nullable: false),
                    End = table.Column<TimeOnly>(type: "time(0)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerAvailabilities", x => x.Id);
                    table.CheckConstraint("CK_Avail_Time", "[Start] < [End]");
                    table.ForeignKey(
                        name: "FK_WorkerAvailabilities_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_SalonId_DayOfWeek",
                table: "WorkingHours",
                columns: new[] { "SalonId", "DayOfWeek" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkingHour_Day",
                table: "WorkingHours",
                sql: "[DayOfWeek] BETWEEN 1 AND 7");

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkingHour_Time",
                table: "WorkingHours",
                sql: "[OpenTime] < [CloseTime]");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOfferings_SalonId_Name",
                table: "ServiceOfferings",
                columns: new[] { "SalonId", "Name" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Service_Duration",
                table: "ServiceOfferings",
                sql: "[DurationMinutes] BETWEEN 5 AND 600");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Service_Price",
                table: "ServiceOfferings",
                sql: "[Price] >= 0");

            migrationBuilder.CreateIndex(
                name: "IX_Salons_Name_Address",
                table: "Salons",
                columns: new[] { "Name", "Address" },
                unique: true,
                filter: "[Address] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Salons_PhoneNumber",
                table: "Salons",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Salon_Name_NotBlank",
                table: "Salons",
                sql: "LEN(LTRIM(RTRIM([Name]))) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Salon_Phone_NotBlank",
                table: "Salons",
                sql: "LEN([PhoneNumber]) >= 7");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceOfferingId",
                table: "Appointments",
                column: "ServiceOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_WorkerId_StartUtc_EndUtc",
                table: "Appointments",
                columns: new[] { "WorkerId", "StartUtc", "EndUtc" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appt_Customer_Start",
                table: "Appointments",
                columns: new[] { "CustomerId", "StartUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_Appt_Salon_Start",
                table: "Appointments",
                columns: new[] { "SalonId", "StartUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSkills_ServiceOfferingId",
                table: "EmployeeSkills",
                column: "ServiceOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SalonId",
                table: "Users",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Avail_Date",
                table: "WorkerAvailabilities",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Avail_Worker_Date",
                table: "WorkerAvailabilities",
                columns: new[] { "WorkerId", "Date" });

            migrationBuilder.CreateIndex(
                name: "UX_Avail_Worker_Date_TimeRange",
                table: "WorkerAvailabilities",
                columns: new[] { "WorkerId", "Date", "Start", "End" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "EmployeeSkills");

            migrationBuilder.DropTable(
                name: "WorkerAvailabilities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_WorkingHours_SalonId_DayOfWeek",
                table: "WorkingHours");

            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkingHour_Day",
                table: "WorkingHours");

            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkingHour_Time",
                table: "WorkingHours");

            migrationBuilder.DropIndex(
                name: "IX_ServiceOfferings_SalonId_Name",
                table: "ServiceOfferings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Service_Duration",
                table: "ServiceOfferings");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Service_Price",
                table: "ServiceOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Salons_Name_Address",
                table: "Salons");

            migrationBuilder.DropIndex(
                name: "IX_Salons_PhoneNumber",
                table: "Salons");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Salon_Name_NotBlank",
                table: "Salons");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Salon_Phone_NotBlank",
                table: "Salons");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Salons");

            migrationBuilder.AlterColumn<int>(
                name: "DayOfWeek",
                table: "WorkingHours",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ServiceOfferings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceOfferings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Salons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Salons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_SalonId",
                table: "WorkingHours",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOfferings_SalonId",
                table: "ServiceOfferings",
                column: "SalonId");
        }
    }
}
