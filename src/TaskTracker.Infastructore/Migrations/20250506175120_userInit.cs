using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Infastructore.Migrations
{
    /// <inheritdoc />
    public partial class userInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Users_UserId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_Users_UserId1",
                table: "TaskComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskParticipants_Users_UserId",
                table: "TaskParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_TeamId1",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "DomainUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TeamId1",
                table: "DomainUsers",
                newName: "IX_DomainUsers_TeamId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DomainUsers",
                table: "DomainUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DomainUsers_Teams_TeamId1",
                table: "DomainUsers",
                column: "TeamId1",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_DomainUsers_UserId",
                table: "Managers",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_DomainUsers_UserId1",
                table: "TaskComments",
                column: "UserId1",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskParticipants_DomainUsers_UserId",
                table: "TaskParticipants",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_DomainUsers_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DomainUsers_Teams_TeamId1",
                table: "DomainUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_DomainUsers_UserId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_DomainUsers_UserId1",
                table: "TaskComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskParticipants_DomainUsers_UserId",
                table: "TaskParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_DomainUsers_UserId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DomainUsers",
                table: "DomainUsers");

            migrationBuilder.RenameTable(
                name: "DomainUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_DomainUsers_TeamId1",
                table: "Users",
                newName: "IX_Users_TeamId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Users_UserId",
                table: "Managers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_Users_UserId1",
                table: "TaskComments",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskParticipants_Users_UserId",
                table: "TaskParticipants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Teams_TeamId1",
                table: "Users",
                column: "TeamId1",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
