using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditColTbCountryWithTbDeliveryInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbDeliveriesInfo_TbCountries_TbCountryCountryId",
                table: "TbDeliveriesInfo");

            migrationBuilder.DropIndex(
                name: "IX_TbDeliveriesInfo_TbCountryCountryId",
                table: "TbDeliveriesInfo");

            migrationBuilder.DropColumn(
                name: "TbCountryCountryId",
                table: "TbDeliveriesInfo");

            migrationBuilder.CreateIndex(
                name: "IX_TbDeliveriesInfo_CountryId",
                table: "TbDeliveriesInfo",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbDeliveriesInfo_TbCountries_CountryId",
                table: "TbDeliveriesInfo",
                column: "CountryId",
                principalTable: "TbCountries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbDeliveriesInfo_TbCountries_CountryId",
                table: "TbDeliveriesInfo");

            migrationBuilder.DropIndex(
                name: "IX_TbDeliveriesInfo_CountryId",
                table: "TbDeliveriesInfo");

            migrationBuilder.AddColumn<int>(
                name: "TbCountryCountryId",
                table: "TbDeliveriesInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbDeliveriesInfo_TbCountryCountryId",
                table: "TbDeliveriesInfo",
                column: "TbCountryCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbDeliveriesInfo_TbCountries_TbCountryCountryId",
                table: "TbDeliveriesInfo",
                column: "TbCountryCountryId",
                principalTable: "TbCountries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
