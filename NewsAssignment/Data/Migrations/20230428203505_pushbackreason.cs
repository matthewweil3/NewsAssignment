using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAssignment.Data.Migrations
{
    /// <inheritdoc />
    public partial class pushbackreason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReturnReason",
                table: "Article",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnReason",
                table: "Article");
        }
    }
}
