using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DedicatedGeo.Mono.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_IMEI",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_IMEI",
                table: "Devices",
                column: "IMEI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Name",
                table: "Devices",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_IMEI",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_Name",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_IMEI",
                table: "Devices",
                column: "IMEI");
        }
    }
}
