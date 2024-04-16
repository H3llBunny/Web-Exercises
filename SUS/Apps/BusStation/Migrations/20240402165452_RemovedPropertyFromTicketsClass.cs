using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusStation.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPropertyFromTicketsClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Tickets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
