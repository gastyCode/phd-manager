using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhDManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "uid",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uid",
                table: "users");
        }
    }
}
