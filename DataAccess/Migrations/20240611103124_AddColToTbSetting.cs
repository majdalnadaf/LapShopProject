using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddColToTbSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShopPageLeftDownSliderImage",
                table: "TbSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopPageLeftDownSliderLink",
                table: "TbSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopPageTopSliderDescription",
                table: "TbSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopPageTopSliderImage",
                table: "TbSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopPageTopSliderLink",
                table: "TbSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopPageTopSliderTitle",
                table: "TbSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CountryName",
                table: "TbCountries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopPageLeftDownSliderImage",
                table: "TbSettings");

            migrationBuilder.DropColumn(
                name: "ShopPageLeftDownSliderLink",
                table: "TbSettings");

            migrationBuilder.DropColumn(
                name: "ShopPageTopSliderDescription",
                table: "TbSettings");

            migrationBuilder.DropColumn(
                name: "ShopPageTopSliderImage",
                table: "TbSettings");

            migrationBuilder.DropColumn(
                name: "ShopPageTopSliderLink",
                table: "TbSettings");

            migrationBuilder.DropColumn(
                name: "ShopPageTopSliderTitle",
                table: "TbSettings");

            migrationBuilder.AlterColumn<string>(
                name: "CountryName",
                table: "TbCountries",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
