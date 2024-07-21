using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;
namespace DataAccess.DaModels.Interfaces
{
    public interface IPurchaseInvoice
    {

        List<TbPurchaseInvoice> GetAll();
        TbPurchaseInvoice GetById(int invoiceId);

        void Save(TbPurchaseInvoice model, List<TbPurchaseInvoiceItem> lstPurchaseInvoicItem, bool isNew);

        bool Delete(int invoiceId);
    }
}
