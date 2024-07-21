using DataAccess;
using Domains.Models;
using Domains.Models.VwAdmin;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.BlHelper.BlAdmin;
using DataAccess.DaModels.Interfaces;
using Microsoft.AspNetCore.Authorization;
using LapShopProject.Filters;
using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;


namespace LapShopProject.Areas.Admin.Controllers
{


    [Area("Admin")]

    [CustomAuthorize]
    public class ItemController : Controller
    {
        #region Ctor
        private readonly IItem clsItem;
        private readonly ICategory clsCategory;
        private readonly IOs clsOs;
        private readonly IItemType clsItemType;
        private readonly IVwItem clsVwItem;

        public ItemController(IItem oItme, ICategory oCategory, IOs oOs, IItemType oItemType, IVwItem oVwItem)
        {
            clsItem = oItme;
            clsCategory = oCategory;
            clsItemType = oItemType;
            clsOs = oOs;
            clsVwItem = oVwItem;

        }
        #endregion


        #region List
        public async Task<IActionResult> List()
        {

            ViewBag.lstCategories = await clsCategory.GetAll();
            var lstItems = clsVwItem.GetAll();
            return View(lstItems);
        }
        #endregion


        #region Edit
        public async Task<IActionResult> Edit(int itemId)
        {

            ViewBag.lstCategories = await clsCategory.GetAll();
            ViewBag.lstItemTypes = await clsItemType.GetAll();
            ViewBag.lstOs = await clsOs.GetAll();

            if (itemId != 0)
            {
                var item = await clsItem.GetById(itemId);
                return View(item);
            }


            return View(new TbItem());
        }
        #endregion

        #region Save

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbItem item, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.lstCategories = await clsCategory.GetAll();
                ViewBag.lstItemTypes = await clsItemType.GetAll();
                ViewBag.lstOs = await clsOs.GetAll();


                return View("Edit", item);
            }

            item.ImageName = await ClsUploadImage.UploadImage(Files, "Items");
            await clsItem.Save(item);

            return RedirectToAction("List");
        }
        #endregion


        #region Search
        public IActionResult Search(int categoryId)
        {


            return View("List", new List<VwItem>());
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int itemId)
        {

            await clsItem.Delete(itemId);
            return RedirectToAction("List");
        } 
        #endregion

    }



}
