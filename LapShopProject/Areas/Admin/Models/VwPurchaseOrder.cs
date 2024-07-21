
using Domains.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShopProject.Areas.Admin.Models
{
    public class VwPurchaseOrder
    {

        [ValidateNever]
        public int InvoiceId { get; set; }
        [Required]
        public int SupplierId { get; set; }

        [ValidateNever]
        
        public string Notes { get; set; } = string.Empty;  

        public List<VwPuchaseOrderItem> lstPurchaseOrderItem { get; set; } = new List<VwPuchaseOrderItem>();    
    }
}
