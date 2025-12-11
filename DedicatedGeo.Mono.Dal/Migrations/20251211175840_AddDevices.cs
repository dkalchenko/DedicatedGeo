using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DedicatedGeo.Mono.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddDevices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "LocationPoints",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "DeviceStatuses",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IMEI = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    QuarantineUntil = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationPoints_DeviceId",
                table: "LocationPoints",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStatuses_DeviceId",
                table: "DeviceStatuses",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_IMEI",
                table: "Devices",
                column: "IMEI");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_QuarantineUntil",
                table: "Devices",
                column: "QuarantineUntil");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStatuses_Devices_DeviceId",
                table: "DeviceStatuses",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocationPoints_Devices_DeviceId",
                table: "LocationPoints",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStatuses_Devices_DeviceId",
                table: "DeviceStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationPoints_Devices_DeviceId",
                table: "LocationPoints");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_LocationPoints_DeviceId",
                table: "LocationPoints");

            migrationBuilder.DropIndex(
                name: "IX_DeviceStatuses_DeviceId",
                table: "DeviceStatuses");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "LocationPoints");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DeviceStatuses");
        }
    }
}
