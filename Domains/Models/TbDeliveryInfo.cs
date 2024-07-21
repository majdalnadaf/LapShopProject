using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    public class TbDeliveryInfo
    {

        public int DeliveryInfoId { get; set; }

        public int DeliveryId { get; set; }

        public int CountryId { get; set; }   
        public int SalesInvoiceId { get; set; }   

        public string StreetName { get; set; }
        public string City { get; set; }    

        public string PostalCode { get; set; }
        public string State { get; set; }

        public virtual TbSalesInvoice TbSalesInvoice { get; set; } = null;
        public virtual TbCountry TbCountry { get; set; } 
        public virtual TbDelivery TbDelivery { get; set; } = null;


    }
}
