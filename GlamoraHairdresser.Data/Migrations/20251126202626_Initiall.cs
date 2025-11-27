using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlamoraHairdresser.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initiall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salons", x => x.Id);
                    table.CheckConstraint("CK_Salon_Name_NotBlank", "LEN(LTRIM(RTRIM([Name]))) > 0");
                    table.CheckConstraint("CK_Salon_Phone_NotBlank", "LEN([PhoneNumber]) >= 7");
                });

            migrationBuilder.CreateTable(
                name: "ServiceOfferings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOfferings", x => x.Id);
                    table.CheckConstraint("CK_Service_Duration", "[DurationMinutes] BETWEEN 5 AND 600");
                    table.CheckConstraint("CK_Service_Price", "[Price] >= 0");
                    table.ForeignKey(
                        name: "FK_ServiceOfferings_Salons_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IterationCount = table.Column<int>(type: "int", nullable: false),
                    Prf = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
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
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalonId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    OpenTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                    table.CheckConstraint("CK_WorkingHour_Day", "[DayOfWeek] BETWEEN 0 AND 6");
                    table.CheckConstraint("CK_WorkingHour_Time", "([IsOpen] = 0) OR ([OpenTime] < [CloseTime])");
                    table.ForeignKey(
                        name: "FK_WorkingHours_Salons_SalonId",
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
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    OpenTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerWorkingHours", x => x.Id);
                    table.CheckConstraint("CK_WorkerWorkingHour_Day", "[DayOfWeek] BETWEEN 0 AND 6");
                    table.CheckConstraint("CK_WorkerWorkingHour_Times", "([IsOpen] = 0 AND [OpenTime] = [CloseTime]) OR ([IsOpen] = 1 AND [OpenTime] < [CloseTime])");
                    table.ForeignKey(
                        name: "FK_WorkerWorkingHours_Users_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOfferings_SalonId_Name",
                table: "ServiceOfferings",
                columns: new[] { "SalonId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialWorkingHours_SalonId_Date",
                table: "SpecialWorkingHours",
                columns: new[] { "SalonId", "Date" },
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_SalonId_DayOfWeek",
                table: "WorkingHours",
                columns: new[] { "SalonId", "DayOfWeek" },
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
                name: "SpecialWorkingHours");

            migrationBuilder.DropTable(
                name: "WorkerAvailabilities");

            migrationBuilder.DropTable(
                name: "WorkerSpecialWorkingHours");

            migrationBuilder.DropTable(
                name: "WorkerWorkingHours");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "ServiceOfferings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Salons");
        }
    }
}
