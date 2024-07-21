using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DaModels
{
    public class ClsSalesInvoiceItems : ISalesInvoiceItems
    {
        private readonly DbLapShopContext context;
        public ClsSalesInvoiceItems(DbLapShopContext ctx)
        {
                context = ctx;
        }


        /// <summary>
        /// Get SalesInvoiceItems of SalesInvoice from database
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns> Returns List of TbSalesInvoiceItem  </returns>
        /// <exception cref="Exception"></exception>
        public List<TbSalesInvoiceItem> GetById(int invoiceId)
        {

           

            try
            {
                var model = context.TbSalesInvoiceItems.Include(s => s.Item).Where(a => a.InvoiceId == invoiceId).ToList();

                if (model == null)
                    return new List<TbSalesInvoiceItem>();

                return model;
            }
            catch (Exception)
            {
                throw new Exception();

            }
        }


        /// <summary>
        /// Save/Update SalesInoivceItems of SalesInvoice in database
        /// </summary>
        /// <param name="items"></param>
        /// <param name="invoiceId"></param>
        /// <param name="isNew"></param>
        /// <returns> Returns true if  the opration success otherwise returns false </returns>
        /// <exception cref="Exception"></exception>
        public bool Save(IList<TbSalesInvoiceItem> items,int invoiceId, bool isNew)
        {
            
            try
            {
                List<TbSalesInvoiceItem> dbSalesInvoiceItems = GetById(items[0].InvoiceId);
                foreach (var interfaceItem in items) 
                {
                    var oDbItem = dbSalesInvoiceItems.Where(a => a.InvoiceItemId == interfaceItem.InvoiceItemId).FirstOrDefault();
                    if (oDbItem == null)
                    {
                        interfaceItem.InvoiceId = invoiceId;
                        context.TbSalesInvoiceItems.Add(interfaceItem);
                    }
                    else 
                    {
                        context.Entry(interfaceItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }


                foreach (var dbItme in dbSalesInvoiceItems)
                {
                    var interfaceItem = items.Where(a => a.InvoiceItemId == dbItme.InvoiceItemId).FirstOrDefault();
                    if (interfaceItem == null)
                    {
                        context.TbSalesInvoiceItems.Remove(dbItme);
                    }
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
    }
}
