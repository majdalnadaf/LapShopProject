using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditTbSlider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdvertisementBanner",
                table: "TbSlider",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AdvertismenetLink",
                table: "TbSlider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "CollectionBanner",
                table: "TbSlider",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HomeSlider",
                table: "TbSlider",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InstegramLink",
                table: "TbSlider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "InstegramSection",
                table: "TbSlider",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvertisementBanner",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "AdvertismenetLink",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "CollectionBanner",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "HomeSlider",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "InstegramLink",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "InstegramSection",
                table: "TbSlider");
        }
    }
}
