using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskBooking.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCoworkingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoworkingId",
                table: "Rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Coworking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coworking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoworkingPhoto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CoworkingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoworkingPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoworkingPhoto_Coworking_CoworkingId",
                        column: x => x.CoworkingId,
                        principalTable: "Coworking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CoworkingId",
                table: "Rooms",
                column: "CoworkingId");

            migrationBuilder.CreateIndex(
                name: "IX_CoworkingPhoto_CoworkingId",
                table: "CoworkingPhoto",
                column: "CoworkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Coworking_CoworkingId",
                table: "Rooms",
                column: "CoworkingId",
                principalTable: "Coworking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Coworking_CoworkingId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "CoworkingPhoto");

            migrationBuilder.DropTable(
                name: "Coworking");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CoworkingId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CoworkingId",
                table: "Rooms");
        }
    }
}
