using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LapShopProject.Areas.Admin.Models
{
    [ValidateNever]
    public class VwPuchaseOrderItem
    {
       
        public int ItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public double Qty { get; set; }

        public decimal Price { get; set; }

         
    }
}
