using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DedicatedGeo.Mono.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceOnlineUpdateAtIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DeviceStatuses_IsDeviceOnline_UpdatedAt",
                table: "DeviceStatuses",
                columns: new[] { "IsDeviceOnline", "UpdatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeviceStatuses_IsDeviceOnline_UpdatedAt",
                table: "DeviceStatuses");
        }
    }
}
