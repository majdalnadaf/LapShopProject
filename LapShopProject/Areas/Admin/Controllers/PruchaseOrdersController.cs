using DataAccess.DaModels.Interfaces;
using Domains.Models;
using LapShopProject.Areas.Admin.Models;
using LapShopProject.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LapShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorize]
    public class PruchaseOrdersController : Controller
    {
        #region Ctor
        private readonly ISupplier clsSupplier;
        private readonly ICategory clsCategory;
        private readonly IPurchaseInvoice clsPurchaseInvoice;
        private readonly IPurchaseInvoiceItem clsPurchaseInvoiceItem;
        public PruchaseOrdersController(ISupplier oSupplier, ICategory oCategory, IPurchaseInvoice oPurchaseInvoice, IPurchaseInvoiceItem oPurchaseInvoiceItem)
        {
            clsSupplier = oSupplier;
            clsCategory = oCategory;
            clsPurchaseInvoice = oPurchaseInvoice;
            clsPurchaseInvoiceItem = oPurchaseInvoiceItem;
        } 
        #endregion


        #region List
        public IActionResult List()
        {


            var lstPurchaseInvoice = clsPurchaseInvoice.GetAll();

            return View(lstPurchaseInvoice);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int invoiceId)
        {

            VwPurchaseOrder oVwPurchaseOrder = new VwPurchaseOrder();

            // for edit
            if (invoiceId != 0)
            {
                var pruchaseInvoice = clsPurchaseInvoice.GetById(invoiceId);
                oVwPurchaseOrder.SupplierId = pruchaseInvoice.SupplierId;
                oVwPurchaseOrder.Notes = pruchaseInvoice.Notes;
                oVwPurchaseOrder.InvoiceId = invoiceId;

                var lstPurchaseInvoiceItem = clsPurchaseInvoiceItem.GetByInvoiceId(invoiceId);

                for (int i = 0; i < lstPurchaseInvoiceItem.Count; i++)
                {
                    oVwPurchaseOrder.lstPurchaseOrderItem.Add(new VwPuchaseOrderItem
                    {
                        ItemId = lstPurchaseInvoiceItem[i].ItemId,
                        Qty = lstPurchaseInvoiceItem[i].Qty,
                        Price = lstPurchaseInvoiceItem[i].InvoicePrice,
                        ItemName = lstPurchaseInvoiceItem[i].Item.ItemName


                    });
                }

            }


            ViewBag.lstSuppliers = await clsSupplier.GetAll();
            ViewBag.lstCategories = await clsCategory.GetAll();


            return View(oVwPurchaseOrder);
        }
        #endregion

        #region Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(VwPurchaseOrder model)
        {


            if (!ModelState.IsValid)
            {

                ViewBag.lstSuppliers = await clsSupplier.GetAll();
                ViewBag.lstCategories = await clsCategory.GetAll();
                return View("Edit", model);
            }



            TbPurchaseInvoice oPurchaseInvoie = new TbPurchaseInvoice();
            oPurchaseInvoie.SupplierId = model.SupplierId;
            oPurchaseInvoie.Notes = model.Notes;


            bool isNew = false;

            if (model.InvoiceId == 0)
                isNew = true;
            else
            {
                oPurchaseInvoie.InvoiceId = model.InvoiceId;
            }


            List<TbPurchaseInvoiceItem> lstPurchaseInvoiceItem = new List<TbPurchaseInvoiceItem>();

            foreach (var VwItem in model.lstPurchaseOrderItem)
            {
                lstPurchaseInvoiceItem.Add(new TbPurchaseInvoiceItem
                {
                    ItemId = VwItem.ItemId,
                    InvoicePrice = VwItem.Price,
                    Qty = VwItem.Qty

                });
            }



            clsPurchaseInvoice.Save(oPurchaseInvoie, lstPurchaseInvoiceItem, isNew);


            return RedirectToAction("List");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int invoiceId)
        {

            if (clsPurchaseInvoiceItem.Delete(invoiceId))
                clsPurchaseInvoice.Delete(invoiceId);



            return RedirectToAction("List");

        } 
        #endregion


    }
}
