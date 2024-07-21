using Domains.Models.VwAdmin;
using Domains.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.BlHelper.BlAdmin;
using DataAccess.DaModels.Interfaces;
using DataAccess.DaModels;
using LapShopProject.Filters;

namespace LapShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorize]
    public class CategoryController : Controller
    {

        #region Ctor
        private readonly ICategory clsCategory;
        public CategoryController(ICategory oCategory)
        {

            clsCategory = oCategory;
        }

        #endregion

        #region List
        public async Task<IActionResult> List()
        {

            var lstCategories = await clsCategory.GetAll();
            return View(lstCategories);
        }
        #endregion


        #region Edit
        public async Task<IActionResult> Edit(int categoryId)
        {
            if (categoryId != 0)
            {
                var category = await clsCategory.GetById(categoryId);
                return View(category);
            }
            return View(new TbCategory());
        }
        #endregion

        #region Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCategory category, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", category);
            }

            category.ImageName = await ClsUploadImage.UploadImage(Files, "Categories");
            category.CurrentState = 1;
            await clsCategory.Save(category);
            return RedirectToAction("List");
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int categoryId)
        {

            await clsCategory.Delete(categoryId);
            return RedirectToAction("List");
        } 
        #endregion
    }
}
