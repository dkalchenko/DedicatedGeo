using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DedicatedGeo.Mono.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_Role",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Role",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_Role",
                table: "Users",
                columns: new[] { "Email", "Role" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email_Role",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Role",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email_Role",
                table: "Users",
                columns: new[] { "Email", "Role" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Role",
                table: "Users",
                column: "Role",
                unique: true);
        }
    }
}
