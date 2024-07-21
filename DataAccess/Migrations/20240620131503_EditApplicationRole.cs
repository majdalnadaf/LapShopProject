using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditApplicationRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleActions",
                table: "AspNetRoles",
                newName: "WebsiteRoleActions");

            migrationBuilder.AddColumn<string>(
                name: "AdminRoleActions",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminRoleActions",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "WebsiteRoleActions",
                table: "AspNetRoles",
                newName: "RoleActions");
        }
    }
}
