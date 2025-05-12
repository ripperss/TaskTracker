using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Infastructore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_DomainUsers_UserId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_DomainUsers_UserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Managers_TeamId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserId",
                table: "Managers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tasks",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                newName: "IX_Tasks_ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Managers_ManagerId",
                table: "Tasks",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Managers_ManagerId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Tasks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ManagerId",
                table: "Tasks",
                newName: "IX_Tasks_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_TeamId",
                table: "Managers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_DomainUsers_UserId",
                table: "Managers",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_DomainUsers_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id");
        }
    }
}
