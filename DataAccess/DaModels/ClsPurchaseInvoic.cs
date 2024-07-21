using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.DaModels
{
    public class ClsPurchaseInvoic : IPurchaseInvoice
    {
        private readonly DbLapShopContext context;

        private readonly IPurchaseInvoiceItem clsPurchaseInvoiceItem;
        
        public ClsPurchaseInvoic(DbLapShopContext ctx, IPurchaseInvoiceItem oPurchaseInvoiceItem)
        {
            context = ctx;
            this.clsPurchaseInvoiceItem = oPurchaseInvoiceItem;
        }

        public List<TbPurchaseInvoice> GetAll()
        {
            try
            {
                var lstPurchaseInvoice = context.TbPurchaseInvoices.Include(a=> a.Supplier).ToList();
                if (lstPurchaseInvoice == null)
                    lstPurchaseInvoice = new List<TbPurchaseInvoice>();


                return lstPurchaseInvoice;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public TbPurchaseInvoice GetById(int invoiceId)
        {
            try
            {

                var purchaseInvoice = context.TbPurchaseInvoices.Where(a => a.InvoiceId == invoiceId).FirstOrDefault();
                if (purchaseInvoice == null)
                    return new TbPurchaseInvoice();

                return purchaseInvoice;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void Save(TbPurchaseInvoice model, List<TbPurchaseInvoiceItem> lstPurchaseInvoicItem ,bool isNew)
        {
            var transaction = context.Database.BeginTransaction();
            try
            {

                model.InvoiceDate = DateTime.Now;

                if (isNew)
                {
                    
                    context.TbPurchaseInvoices.Add(model); 

                }
                else
                {
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                context.SaveChanges();


                clsPurchaseInvoiceItem.Save(lstPurchaseInvoicItem, model.InvoiceId);


               transaction.Commit();    

                 
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw new Exception(e.Message);
            }
        }


        public bool Delete(int invoiceId) 
        {


            try
            {

                if (invoiceId == 0)
                    return false;

                var purchaseInvoice = GetById(invoiceId);
                if (purchaseInvoice == null)
                    return false;


                context.TbPurchaseInvoices.Remove(purchaseInvoice);
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
