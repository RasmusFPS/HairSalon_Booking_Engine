using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSalon_Booking_Engine.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVarNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Treatments",
                newName: "DurationMin");

            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "Bookings",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "BookedDate",
                table: "Bookings",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationMin",
                table: "Treatments",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Bookings",
                newName: "BookingDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Bookings",
                newName: "BookedDate");
        }
    }
}
