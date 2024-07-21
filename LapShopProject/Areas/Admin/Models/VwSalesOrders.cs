namespace LapShopProject.Areas.Admin.Models
{
    public class VwSalesOrders
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    
        public string CountryName { get; set; } = string.Empty;

        public string DeliverName { get; set; } = string.Empty;

    }
}
