using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HairSalon_Booking_Engine.Migrations
{
    /// <inheritdoc />
    public partial class ScheduleRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DropColumn(
                name: "Available",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "LunchTime",
                table: "Schedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "WorkEnd",
                table: "Schedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "WorkStart",
                table: "Schedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DayOfWeek", "LunchTime", "WorkEnd", "WorkStart" },
                values: new object[] { 1, new TimeOnly(12, 0, 0), new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DayOfWeek", "LunchTime", "WorkEnd", "WorkStart" },
                values: new object[] { 4, new TimeOnly(12, 0, 0), new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DayOfWeek", "LunchTime", "StylistId", "WorkEnd", "WorkStart" },
                values: new object[] { 3, new TimeOnly(12, 0, 0), 2, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DayOfWeek", "LunchTime", "StylistId", "WorkEnd", "WorkStart" },
                values: new object[] { 3, new TimeOnly(12, 0, 0), 2, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DayOfWeek", "LunchTime", "StylistId", "WorkEnd", "WorkStart" },
                values: new object[] { 1, new TimeOnly(12, 0, 0), 3, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DayOfWeek", "LunchTime", "StylistId", "WorkEnd", "WorkStart" },
                values: new object[] { 4, new TimeOnly(12, 0, 0), 3, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "LunchTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "WorkEnd",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "WorkStart",
                table: "Schedules");

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Schedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Available", "EndTime", "Notes", "StartTime" },
                values: new object[] { true, new DateTime(2025, 5, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 12, 9, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Available", "EndTime", "Notes", "StartTime" },
                values: new object[] { false, new DateTime(2025, 5, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), "Booked – Emma Johansson", new DateTime(2025, 5, 12, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Available", "EndTime", "Notes", "StartTime", "StylistId" },
                values: new object[] { true, new DateTime(2025, 5, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Available", "EndTime", "Notes", "StartTime", "StylistId" },
                values: new object[] { true, new DateTime(2025, 5, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Available", "EndTime", "Notes", "StartTime", "StylistId" },
                values: new object[] { false, new DateTime(2025, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Booked – Maja Nilsson", new DateTime(2025, 5, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Available", "EndTime", "Notes", "StartTime", "StylistId" },
                values: new object[] { true, new DateTime(2025, 5, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "Available", "EndTime", "Notes", "StartTime", "StylistId" },
                values: new object[,]
                {
                    { 7, true, new DateTime(2025, 5, 14, 13, 30, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, false, new DateTime(2025, 5, 14, 14, 0, 0, 0, DateTimeKind.Unspecified), "Booked – Lucas Berg", new DateTime(2025, 5, 14, 13, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 9, true, new DateTime(2025, 5, 14, 17, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 14, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 10, true, new DateTime(2025, 5, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 21, 9, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 11, false, new DateTime(2025, 5, 21, 16, 15, 0, 0, DateTimeKind.Unspecified), "Booked – Astrid Eriksson", new DateTime(2025, 5, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 12, true, new DateTime(2025, 5, 21, 17, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 21, 16, 15, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 13, false, new DateTime(2025, 5, 19, 10, 30, 0, 0, DateTimeKind.Unspecified), "Booked – Oliver Svensson", new DateTime(2025, 5, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 14, true, new DateTime(2025, 5, 19, 17, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 19, 10, 30, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 15, true, new DateTime(2025, 5, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 16, false, new DateTime(2025, 5, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), "Booked – Emma Johansson", new DateTime(2025, 5, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 17, true, new DateTime(2025, 5, 22, 17, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });
        }
    }
}
