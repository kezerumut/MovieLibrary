using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDBApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReleaseDateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Movies0",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Point",
                table: "Movies0",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReleaseDate",
                table: "Movies0",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Point",
                table: "Movies0",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
