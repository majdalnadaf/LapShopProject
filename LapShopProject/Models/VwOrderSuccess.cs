using DataAccess.Identity;
using Domains.Models;
using Microsoft.Identity.Client;

namespace LapShopProject.Models
{
    /// <summary>
    /// View model of Order Success Page
    /// </summary>
    public class VwOrderSuccess
    {
        public TbCashTransacion CashTransaction { get; set; } = new TbCashTransacion();
        public TbSalesInvoice SalesInvoice { get; set; } = new TbSalesInvoice();
        public List<TbSalesInvoiceItem> lstSalesInoivceItems { get; set; } = new List<TbSalesInvoiceItem>();
        public TbDeliveryInfo DeliveryInfo { get; set; } = new TbDeliveryInfo();    
        public decimal SubTotalPrice { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalPrice { get; set; }
        public ApplicationUser User { get; set; } = new ApplicationUser();
    }
}
