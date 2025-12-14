using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DedicatedGeo.Mono.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_QuarantineUntil",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "QuarantineUntil",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DeviceAssignments",
                columns: table => new
                {
                    DeviceAssignmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceAssignments", x => x.DeviceAssignmentId);
                    table.ForeignKey(
                        name: "FK_DeviceAssignments_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAssignments_DeviceId",
                table: "DeviceAssignments",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAssignments_UserId",
                table: "DeviceAssignments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStatusHistories_Devices_DeviceId",
                table: "DeviceStatusHistories",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStatusHistories_Devices_DeviceId",
                table: "DeviceStatusHistories");

            migrationBuilder.DropTable(
                name: "DeviceAssignments");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "QuarantineUntil",
                table: "Devices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_QuarantineUntil",
                table: "Devices",
                column: "QuarantineUntil");
        }
    }
}
