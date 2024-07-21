using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Domains.Models.VwAdmin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DaModels
{
    public class ClsSalesInvoice : ISalesInvoice
    {

        private readonly ISalesInvoiceItems clsSalesInvoiceItem;
        private readonly IDeliveryInfo clsDeliveryInfo;

        private readonly DbLapShopContext context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public ClsSalesInvoice(DbLapShopContext ctx, UserManager<ApplicationUser> userManager, ISalesInvoiceItems oSalesInvoiceItem, IDeliveryInfo oDeliveryInfo ,
            SignInManager<ApplicationUser> signInManager)
        {
            context = ctx;
            _userManager = userManager;
            clsSalesInvoiceItem = oSalesInvoiceItem;
            clsDeliveryInfo = oDeliveryInfo;
            _signInManager = signInManager; 
        }


        public bool Delete(int id)
        {
            try
            {
                var salesInvoice = context.TbSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
                if (salesInvoice == null)
                    return false;

                context.TbSalesInvoices.Remove(salesInvoice);
                context.SaveChanges();
                return true;


            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public List<TbSalesInvoice> GetAll()
        {
            try
            {
                return context.TbSalesInvoices.ToList();
            }
            catch (Exception ex)
            {
                return new List<TbSalesInvoice>();
            }
        } 
        
        public List<TbSalesInvoice> GetAllByUserId(string userId)
        {
            try
            {
                var lstSalesInvoice =  context.TbSalesInvoices.Include(a=> a.TbDelivryInfo).Where(a=> a.CreatedBy == userId).ToList();
                if(lstSalesInvoice==null)
                    return new List<TbSalesInvoice>();

                return lstSalesInvoice;
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TbSalesInvoice GetById(int id)
        {
            try
            {
                var salesInvoice = context.TbSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();

                if (salesInvoice == null)
                    return new TbSalesInvoice();

                return salesInvoice;
            }
            catch (Exception ex)
            {
                return new TbSalesInvoice();

            }
        }

        public async Task<int> Save(TbSalesInvoice model, List<TbSalesInvoiceItem> lstSalesInvoiceItems, TbDeliveryInfo deliveryInfo, ApplicationUser user , bool isNew)
        {
            var transaction = context.Database.BeginTransaction();

            try
            {


                model.CurrentState = 1;
                model.InvoiceDate = DateTime.Now;
                model.DelivryDate = DateTime.Now.AddDays(15);// get the number of days from TbSetting

                if (isNew)
                {
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = user.Id;
                    model.UpdatedDate = DateTime.Now;

                    // Add in Database
                    context.TbSalesInvoices.Add(model);
                }
                else
                {
                    model.UpdatedBy = user.Id;
                    model.UpdatedDate = DateTime.Now;

                    //Update Model in Database
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                //Save SalesInvoice
                context.SaveChanges();

                //Save SalesInvoiceItems
                clsSalesInvoiceItem.Save(lstSalesInvoiceItems.ToList(), model.InvoiceId, true);

                // relate the deliveryInfo with its salesInovice
                deliveryInfo.SalesInvoiceId = model.InvoiceId;

                //Save deliveryInfo
                clsDeliveryInfo.SaveWithoutAsync(deliveryInfo);

                transaction.Commit();
                return model.InvoiceId;

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception( ex.Message);
            }
        }
    }
}
