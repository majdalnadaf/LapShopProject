using DataAccess.DaModels.Interfaces;
using Domains.Models;
using LapShopProject.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LapShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CustomAuthorize]
    public class AdditionalPagesController : Controller
    {
        #region Ctor
        private readonly IPage clsPage;
        public AdditionalPagesController(IPage oPage)
        {
            clsPage = oPage;
        }
        #endregion



        #region List
        public IActionResult List()
        {

            var lstPages = clsPage.GetAll();

            return View(lstPages);
        }
        #endregion


        #region Edit
        public IActionResult Edit(int pageId)
        {
            var oPage = new TbPage();

            if (pageId != 0)
            {
                oPage = clsPage.GetById(pageId);
            }


            return View(oPage);
        }
        #endregion


        #region Save

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(TbPage model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            model.CssContent = (model.CssContent == null) ? string.Empty : model.CssContent;
            model.MetaTagsHtml = (model.MetaTagsHtml == null) ? string.Empty : model.MetaTagsHtml;
            model.JsContent = (model.JsContent == null) ? string.Empty : model.JsContent;


            if (!clsPage.Save(model))
                return View("Edit", model);


            return RedirectToAction("List");
        }

        #endregion

        #region Delete
        public IActionResult Delete(int pageId)
        {

            clsPage.Delete(pageId);
            return RedirectToAction("List");
        } 
        #endregion





    }
}
