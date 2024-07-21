using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTbDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbDeliveriesInfo",
                columns: table => new
                {
                    DeliveryInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesInvoiceId = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TbCountryCountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbDeliveriesInfo", x => x.DeliveryInfoId);
                    table.ForeignKey(
                        name: "FK_TbDeliveriesInfo_TbCountries_TbCountryCountryId",
                        column: x => x.TbCountryCountryId,
                        principalTable: "TbCountries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbDeliveriesInfo_TbDelivery_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "TbDelivery",
                        principalColumn: "DeliveryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbDeliveriesInfo_DeliveryId",
                table: "TbDeliveriesInfo",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_TbDeliveriesInfo_TbCountryCountryId",
                table: "TbDeliveriesInfo",
                column: "TbCountryCountryId");

   
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "TbDeliveriesInfo");
        }
    }
}
