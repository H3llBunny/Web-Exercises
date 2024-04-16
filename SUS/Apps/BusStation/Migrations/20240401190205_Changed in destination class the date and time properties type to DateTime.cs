using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusStation.Migrations
{
    /// <inheritdoc />
    public partial class ChangedInDestinationClassTheDateAndTimePropertiesTypeToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Destinations",
                type: "datetime2",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Destinations",
                type: "datetime2",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldMaxLength: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Time",
                table: "Destinations",
                type: "time",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Destinations",
                type: "date",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 30);
        }
    }
}
