using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface ISalesInvoiceItems
    {
        bool Save(IList<TbSalesInvoiceItem> items, int invoiceId, bool isNew);

        List<TbSalesInvoiceItem> GetById(int invoiceId);

    }
}
