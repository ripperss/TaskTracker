using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Infastructore.Migrations
{
    /// <inheritdoc />
    public partial class ChangeManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_DomainUsers_UserId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Teams_TeamId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_TeamId",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_UserId",
                table: "Managers");
        }
    }
}
