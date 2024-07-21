using DataAccess.DaModels.Interfaces;
using Domains.Models.VwAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsVwSalesInvoice : IVwSalesInvoice
    {
        private readonly DbLapShopContext context;
        public ClsVwSalesInvoice(DbLapShopContext ctx)
        {
            context = ctx;
        }

        public List<VwSalesInvoice> GetAll()
        {
            try
            {
                return context.VwSalesInvoices.ToList();
            }
            catch (Exception e )
            {

                throw new Exception(e.Message);
            }
        }

        public VwSalesInvoice GetById(int id)
        {
            try
            {
                var model = GetAll().Where(a => a.InvoiceId == id).FirstOrDefault();
                if (model == null)
                    return new VwSalesInvoice();

                return model;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
