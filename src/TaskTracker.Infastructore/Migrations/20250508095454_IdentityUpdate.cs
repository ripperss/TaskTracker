﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Infastructore.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "UsersApplication");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UsersApplication",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
