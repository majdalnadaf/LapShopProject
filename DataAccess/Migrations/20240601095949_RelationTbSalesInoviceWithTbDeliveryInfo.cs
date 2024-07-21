using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RelationTbSalesInoviceWithTbDeliveryInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
            name: "FK_TbSalesInvoices_TbDeliveryInfo_DelivryInfoId",
            table: "TbSalesInvoices",
            column: "DelivryInfoId",
            principalTable: "TbDeliveryInfo",
            principalColumn: "DeliveryInfoId",
            onDelete: ReferentialAction.Cascade);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_TbSalesInvoices_TbDeliveryInfo_DelivryInfoId",
            table: "TbSalesInvoices");

        }
    }
}
