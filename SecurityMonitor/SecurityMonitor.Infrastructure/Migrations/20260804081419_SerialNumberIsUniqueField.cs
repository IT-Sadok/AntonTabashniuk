using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SerialNumberIsUniqueField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_devices_serial_number",
                table: "devices",
                column: "serial_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_devices_serial_number",
                table: "devices");
        }
    }
}
