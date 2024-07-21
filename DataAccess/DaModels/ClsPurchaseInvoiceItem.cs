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
    public class ClsPurchaseInvoiceItem : IPurchaseInvoiceItem
    {

        private readonly DbLapShopContext context;
        public ClsPurchaseInvoiceItem(DbLapShopContext ctx)
        {
            context = ctx;
        }

        /// <summary>
        /// Get all PurchaseInvoiceItem of specific Purchase Invoice form database
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<TbPurchaseInvoiceItem> GetByInvoiceId(int invoiceId)
        {

            try
            {
                var lstPurchaseInvoiceItem = context.TbPurchaseInvoiceItems.Include(a => a.Item).Where(a => a.InvoiceId == invoiceId).ToList();
                if (lstPurchaseInvoiceItem == null)
                    return new List<TbPurchaseInvoiceItem>();

                return lstPurchaseInvoiceItem;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Save/Update TbPurchaseInvoiceItem in database
        /// </summary>
        /// <param name="lstPurchaseInvoiceItem"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Save(List<TbPurchaseInvoiceItem> lstPurchaseInvoiceItem, int invoiceId)
        {
            try
            {

                if (lstPurchaseInvoiceItem.Count == 0)
                    return false;


                List<TbPurchaseInvoiceItem> lstDBPItems = GetByInvoiceId(invoiceId);

                foreach( var interfaceItem in lstPurchaseInvoiceItem)
                {
                    var oDbItem = context.TbPurchaseInvoiceItems.Where(a => a.InvoiceItemId == interfaceItem.InvoiceItemId).FirstOrDefault();
                    if (oDbItem == null)
                    {
                        interfaceItem.InvoiceId = invoiceId;
                        context.TbPurchaseInvoiceItems.Add(interfaceItem);
                    }
                    else 
                    {
                        context.Entry(interfaceItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }


                foreach(var dbItem in lstDBPItems)
                {
                    var interfaceItem = lstPurchaseInvoiceItem.Where(a => a.InvoiceItemId == dbItem.InvoiceItemId).FirstOrDefault();
                    if(interfaceItem == null)
                    {
                        context.TbPurchaseInvoiceItems.Remove(dbItem);
                    }
                }


                context.SaveChanges();
                return true;


            }
            catch (Exception  e)
            {

                throw new Exception(e.Message);
            }
        }


        public bool Delete(int invoiceId)
        {
            try
            {
                var lstPurchaseInvoiceItem = GetByInvoiceId(invoiceId);

                if (lstPurchaseInvoiceItem == null)
                    return false;

                foreach(var Item in lstPurchaseInvoiceItem)
                {
                    context.TbPurchaseInvoiceItems.Remove(Item);
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
