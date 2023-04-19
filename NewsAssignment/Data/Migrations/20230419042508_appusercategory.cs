using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAssignment.Data.Migrations
{
    /// <inheritdoc />
    public partial class appusercategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserCategory_AspNetUsers_ApplicationUsersId",
                table: "ApplicationUserCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserCategory_Category_CategoriesCategoryId",
                table: "ApplicationUserCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserCategory",
                table: "ApplicationUserCategory");

            migrationBuilder.DropColumn(
                name: "ApplicationUsersId",
                table: "ApplicationUserCategory");

            migrationBuilder.RenameColumn(
                name: "CategoriesCategoryId",
                table: "ApplicationUserCategory",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserCategory_CategoriesCategoryId",
                table: "ApplicationUserCategory",
                newName: "IX_ApplicationUserCategory_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ApplicationUserCategory",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "ApplicationUserCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "ApplicationUserCategory",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserCategory",
                table: "ApplicationUserCategory",
                column: "ID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserCategory_Category_CategoryId",
                table: "ApplicationUserCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserCategory_AspNetUsers_ApplicationUserId1",
                table: "ApplicationUserCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserCategory_Category_CategoryId",
                table: "ApplicationUserCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserCategory",
                table: "ApplicationUserCategory");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserCategory_ApplicationUserId1",
                table: "ApplicationUserCategory");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ApplicationUserCategory");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ApplicationUserCategory");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "ApplicationUserCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ApplicationUserCategory",
                newName: "CategoriesCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserCategory_CategoryId",
                table: "ApplicationUserCategory",
                newName: "IX_ApplicationUserCategory_CategoriesCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUsersId",
                table: "ApplicationUserCategory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserCategory",
                table: "ApplicationUserCategory",
                columns: new[] { "ApplicationUsersId", "CategoriesCategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserCategory_AspNetUsers_ApplicationUsersId",
                table: "ApplicationUserCategory",
                column: "ApplicationUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserCategory_Category_CategoriesCategoryId",
                table: "ApplicationUserCategory",
                column: "CategoriesCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
