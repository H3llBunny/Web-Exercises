﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoiteRecepti.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Images",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Images");
        }
    }
}
