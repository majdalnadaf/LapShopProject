using DataAccess.DaModels.Interfaces;
using Domains.Models.VwAdmin;
using LapShopProject.Areas.Admin.Models;
using LapShopProject.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LapShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorize]
    public class SalesOrdersController : Controller
    {
        #region Ctor
        private readonly IVwSalesInvoice clsSalesInvoice;
        private readonly ISalesInvoiceItems clsSalesInvoiceItems;
        public SalesOrdersController(IVwSalesInvoice oSalesInvoice, ISalesInvoiceItems oSalesInvoiceItems)
        {
            clsSalesInvoice = oSalesInvoice;
            clsSalesInvoiceItems = oSalesInvoiceItems;
        } 
        #endregion


        #region List
        public IActionResult List()
        {
            List<VwSalesOrders> lstVwSalesOrders = new List<VwSalesOrders>();
            List<VwSalesInvoice> lstSalesInvoice = clsSalesInvoice.GetAll();

            for (int i = 0; i < lstSalesInvoice.Count; i++)
            {
                lstVwSalesOrders.Add(new VwSalesOrders
                {
                    InvoiceId = lstSalesInvoice[i].InvoiceId,
                    InvoiceDate = lstSalesInvoice[i].InvoiceDate,
                    CountryName = lstSalesInvoice[i].CountryName,
                    CustomerName = lstSalesInvoice[i].FirstName + lstSalesInvoice[i].LastName,
                    CreatedBy = lstSalesInvoice[i].CreatedBy,
                    CreatedDate = lstSalesInvoice[i].CreatedDate,
                    DeliverName = lstSalesInvoice[i].DeliveryName
                });
            }



            return View(lstVwSalesOrders);
        } 
        #endregion


        #region Details
        public IActionResult Details(int invoiceId)
        {

            VwSalesInvoice salesInvoice = clsSalesInvoice.GetById(invoiceId);
            var lstSalesInvoiceItems = clsSalesInvoiceItems.GetById(invoiceId);

            if (lstSalesInvoiceItems == null)
                lstSalesInvoiceItems = new List<Domains.Models.TbSalesInvoiceItem>();

            ViewBag.SalesInvoiceItems = lstSalesInvoiceItems;


            return View(salesInvoice);
        } 
        #endregion


    }
}
