using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAssignment.Data.Migrations
{
    /// <inheritdoc />
    public partial class editors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Article",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Article");
        }
    }
}
