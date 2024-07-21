using System;
using System.Collections.Generic;

namespace Domains.Models.VwAdmin;
public partial class VwSalesInvoice
{
    public int InvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }

    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string StreetName { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;

    public decimal CostDelivery { get; set; }
    public string DeliveryName { get; set; } = string.Empty;


}
