using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVwItemView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"alter view VwItems
as
SELECT dbo.TbItems.ItemName,dbo.TbItems.Rating, dbo.TbItems.PurchasePrice, dbo.TbItems.SalesPrice, dbo.TbItems.CategoryId, dbo.TbItems.ImageName, dbo.TbItems.CreatedDate, dbo.TbItems.CreatedBy, dbo.TbItems.CurrentState, dbo.TbItems.UpdatedBy, 
                  dbo.TbItems.UpdatedDate, dbo.TbItems.Description, dbo.TbItems.Gpu, dbo.TbItems.HardDisk, dbo.TbItems.ItemTypeId, dbo.TbItems.Processor, dbo.TbItems.RamSize, dbo.TbItems.ScreenReslution, dbo.TbItems.ScreenSize, 
                  dbo.TbItems.Weight, dbo.TbItems.OsId, dbo.TbCategories.CategoryName, dbo.TbItemTypes.ItemTypeName, dbo.TbOs.OsName, dbo.TbItems.ItemId
FROM     dbo.TbItems INNER JOIN
                  dbo.TbCategories ON dbo.TbItems.CategoryId = dbo.TbCategories.CategoryId INNER JOIN
                  dbo.TbItemTypes ON dbo.TbItems.ItemTypeId = dbo.TbItemTypes.ItemTypeId INNER JOIN
                  dbo.TbOs ON dbo.TbItems.OsId = dbo.TbOs.OsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwItems");
        }
    }
}
