using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DedicatedGeo.Mono.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceStatusHystories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeviceOnline",
                table: "DeviceStatuses",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Devices",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceStatusHistories",
                columns: table => new
                {
                    DeviceStatusHistoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StatusName = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    OldValue = table.Column<string>(type: "TEXT", maxLength: 40, nullable: true),
                    NewValue = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatusHistories", x => x.DeviceStatusHistoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStatusHistories_DeviceId_StatusName_ChangedAt",
                table: "DeviceStatusHistories",
                columns: new[] { "DeviceId", "StatusName", "ChangedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceStatusHistories");

            migrationBuilder.DropColumn(
                name: "IsDeviceOnline",
                table: "DeviceStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Devices",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
