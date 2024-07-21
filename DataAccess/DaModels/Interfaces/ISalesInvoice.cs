using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Identity;
using Domains.Models;
using Domains.Models.VwAdmin;

namespace DataAccess.DaModels.Interfaces
{
    public interface ISalesInvoice
    {
        List<TbSalesInvoice> GetAll();
        TbSalesInvoice GetById(int id);
        List<TbSalesInvoice> GetAllByUserId(string userId);
        Task<int> Save(TbSalesInvoice model, List<TbSalesInvoiceItem> lstSalesInvoiceItems, TbDeliveryInfo deliveryInfo, ApplicationUser user, bool isNew);
        bool Delete(int id);
    }
}
