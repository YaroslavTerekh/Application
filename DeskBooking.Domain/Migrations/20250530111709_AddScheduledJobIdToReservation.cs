using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskBooking.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduledJobIdToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScheduledJob",
                table: "Reservations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledJob",
                table: "Reservations");
        }
    }
}
