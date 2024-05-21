using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.API.Migrations
{
    /// <inheritdoc />
    public partial class Initialise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Titles_UserCurrentTitleTitleID",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserCurrentTitleTitleID",
                table: "Users",
                newName: "UserCurrentTitleTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserCurrentTitleTitleID",
                table: "Users",
                newName: "IX_Users_UserCurrentTitleTitleId");

            migrationBuilder.RenameColumn(
                name: "TitleID",
                table: "Titles",
                newName: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Titles_UserCurrentTitleTitleId",
                table: "Users",
                column: "UserCurrentTitleTitleId",
                principalTable: "Titles",
                principalColumn: "TitleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Titles_UserCurrentTitleTitleId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserCurrentTitleTitleId",
                table: "Users",
                newName: "UserCurrentTitleTitleID");

            migrationBuilder.RenameIndex(
                name: "IX_Users_UserCurrentTitleTitleId",
                table: "Users",
                newName: "IX_Users_UserCurrentTitleTitleID");

            migrationBuilder.RenameColumn(
                name: "TitleId",
                table: "Titles",
                newName: "TitleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Titles_UserCurrentTitleTitleID",
                table: "Users",
                column: "UserCurrentTitleTitleID",
                principalTable: "Titles",
                principalColumn: "TitleID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
