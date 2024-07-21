using Domains.Models.VwAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels.Interfaces
{
    public interface IVwSalesInvoice
    {
        List<VwSalesInvoice> GetAll();
        VwSalesInvoice GetById(int id);
    }
}
