using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAssignment.Data.Migrations
{
    /// <inheritdoc />
    public partial class newappusercat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserCategory_AspNetUsers_ApplicationUserId1",
                table: "ApplicationUserCategory");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserCategory_ApplicationUserId1",
                table: "ApplicationUserCategory");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "ApplicationUserCategory");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ApplicationUserCategory",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCategory_ApplicationUserId",
                table: "ApplicationUserCategory",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserCategory_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserCategory",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserCategory_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserCategory");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserCategory_ApplicationUserId",
                table: "ApplicationUserCategory");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "ApplicationUserCategory",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "ApplicationUserCategory",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCategory_ApplicationUserId1",
                table: "ApplicationUserCategory",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserCategory_AspNetUsers_ApplicationUserId1",
                table: "ApplicationUserCategory",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
