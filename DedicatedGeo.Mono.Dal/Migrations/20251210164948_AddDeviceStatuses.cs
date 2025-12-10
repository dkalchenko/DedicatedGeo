using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DedicatedGeo.Mono.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceStatuses",
                columns: table => new
                {
                    DeviceStatusId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BatteryLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    IsInAlarm = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsButtonPressed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsInCharge = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsGPSOnline = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatuses", x => x.DeviceStatusId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceStatuses");
        }
    }
}
