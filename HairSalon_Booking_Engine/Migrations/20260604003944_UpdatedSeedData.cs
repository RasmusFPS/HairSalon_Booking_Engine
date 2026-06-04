using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HairSalon_Booking_Engine.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.InsertData(
                table: "BookingTreatment",
                columns: new[] { "BookingsId", "TreatmentsId" },
                values: new object[,]
                {
                    { 3, 3 },
                    { 4, 6 },
                    { 5, 2 },
                    { 6, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Status" },
                values: new object[] { new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Status", "StylistId" },
                values: new object[] { new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status" },
                values: new object[] { new DateTime(2026, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 6, 1, 15, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status" },
                values: new object[] { new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 6, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status", "StylistId" },
                values: new object[] { new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 6, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status", "StylistId" },
                values: new object[] { new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 6, 2, 10, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 2, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status", "StylistId" },
                values: new object[,]
                {
                    { 7, new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 6, 2, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 8, new DateTime(2026, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 6, 3, 10, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 9, new DateTime(2026, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 6, 3, 13, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 3, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 10, new DateTime(2026, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 6, 3, 15, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 3, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 11, new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 6, 4, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 12, new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 6, 4, 13, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 4, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 13, new DateTime(2026, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 6, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 14, new DateTime(2026, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 6, 4, 15, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 4, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 15, new DateTime(2026, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 6, 5, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 16, new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 6, 5, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 17, new DateTime(2026, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 6, 5, 13, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 5, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 18, new DateTime(2026, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 6, 8, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 19, new DateTime(2026, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 6, 8, 10, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 20, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 6, 8, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 8, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 21, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 6, 11, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 22, new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 6, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 23, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 6, 11, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 24, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 6, 11, 15, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 11, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 25, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 6, 12, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 26, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 6, 12, 9, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 27, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 6, 12, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 12, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 28, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 6, 15, 10, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 29, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 6, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 30, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 6, 15, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 0, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2,
                column: "DayOfWeek",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DayOfWeek", "StylistId" },
                values: new object[] { 4, 1 });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DayOfWeek", "StylistId" },
                values: new object[] { 5, 2 });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 6,
                column: "DayOfWeek",
                value: 1);

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "DayOfWeek", "LunchTime", "StylistId", "WorkEnd", "WorkStart" },
                values: new object[,]
                {
                    { 7, 4, new TimeOnly(12, 0, 0), 3, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) },
                    { 8, 5, new TimeOnly(12, 0, 0), 3, new TimeOnly(17, 0, 0), new TimeOnly(9, 0, 0) }
                });

            migrationBuilder.UpdateData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Children's Cut (<=12)");

            migrationBuilder.InsertData(
                table: "BookingTreatment",
                columns: new[] { "BookingsId", "TreatmentsId" },
                values: new object[,]
                {
                    { 7, 1 },
                    { 8, 5 },
                    { 9, 2 },
                    { 10, 8 },
                    { 11, 7 },
                    { 12, 9 },
                    { 13, 1 },
                    { 14, 5 },
                    { 15, 6 },
                    { 16, 3 },
                    { 17, 2 },
                    { 18, 1 },
                    { 19, 4 },
                    { 20, 6 },
                    { 21, 3 },
                    { 22, 1 },
                    { 23, 3 },
                    { 24, 8 },
                    { 25, 3 },
                    { 26, 9 },
                    { 27, 4 },
                    { 28, 5 },
                    { 29, 2 },
                    { 30, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 10, 8 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 11, 7 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 12, 9 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 14, 5 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 15, 6 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 16, 3 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 17, 2 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 19, 4 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 20, 6 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 21, 3 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 22, 1 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 23, 3 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 24, 8 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 25, 3 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 26, 9 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 27, 4 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 28, 5 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 29, 2 });

            migrationBuilder.DeleteData(
                table: "BookingTreatment",
                keyColumns: new[] { "BookingsId", "TreatmentsId" },
                keyValues: new object[] { 30, 7 });

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.InsertData(
                table: "BookingTreatment",
                columns: new[] { "BookingsId", "TreatmentsId" },
                values: new object[,]
                {
                    { 1, 8 },
                    { 3, 1 },
                    { 3, 6 },
                    { 4, 3 },
                    { 5, 5 },
                    { 6, 7 }
                });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Status" },
                values: new object[] { new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndTime", "StartTime", "Status", "StylistId" },
                values: new object[] { new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 14, 13, 30, 0, 0, DateTimeKind.Unspecified), 0, 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status" },
                values: new object[] { new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status" },
                values: new object[] { new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status", "StylistId" },
                values: new object[] { new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), 0, 2 });

            migrationBuilder.UpdateData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "CustomerId", "EndTime", "StartTime", "Status", "StylistId" },
                values: new object[] { new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 22, 10, 30, 0, 0, DateTimeKind.Unspecified), 0, 3 });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2,
                column: "DayOfWeek",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DayOfWeek", "StylistId" },
                values: new object[] { 3, 2 });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DayOfWeek", "StylistId" },
                values: new object[] { 1, 3 });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 6,
                column: "DayOfWeek",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Children's Cut (≤12)");
        }
    }
}
