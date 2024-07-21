using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IPurchaseInvoiceItem
    {
        List<TbPurchaseInvoiceItem> GetByInvoiceId(int invoiceId);

        bool Save(List<TbPurchaseInvoiceItem> lstPurchaseInvoiceItem , int invoiceId);

        bool Delete(int invoiceId); 

    }
}
