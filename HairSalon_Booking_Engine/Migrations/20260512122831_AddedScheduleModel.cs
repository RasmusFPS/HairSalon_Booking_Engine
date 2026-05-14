using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HairSalon_Booking_Engine.Migrations
{
    /// <inheritdoc />
    public partial class AddedScheduleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StylistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Stylist_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "Available", "EndTime", "Notes", "StartTime", "StylistId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 5, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, false, new DateTime(2025, 5, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), "Booked – Emma Johansson", new DateTime(2025, 5, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, true, new DateTime(2025, 5, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 12, 11, 30, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, true, new DateTime(2025, 5, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, false, new DateTime(2025, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Booked – Maja Nilsson", new DateTime(2025, 5, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, true, new DateTime(2025, 5, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
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

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_StylistId",
                table: "Schedules",
                column: "StylistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
