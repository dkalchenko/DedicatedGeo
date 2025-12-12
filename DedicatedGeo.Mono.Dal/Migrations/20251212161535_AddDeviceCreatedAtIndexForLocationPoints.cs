using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DedicatedGeo.Mono.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceCreatedAtIndexForLocationPoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LocationPoints_DeviceId",
                table: "LocationPoints");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPoints_DeviceId_CreatedAt",
                table: "LocationPoints",
                columns: new[] { "DeviceId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LocationPoints_DeviceId_CreatedAt",
                table: "LocationPoints");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPoints_DeviceId",
                table: "LocationPoints",
                column: "DeviceId");
        }
    }
}
