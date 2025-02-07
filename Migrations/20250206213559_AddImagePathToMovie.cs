﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDBApp.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Movies0",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Movies0");
        }
    }
}
