using DataAccess.DaModels.Interfaces;
using Domains.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.BlHelper.BlAdmin;
using LapShopProject.Filters;

namespace LapShopProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [CustomAuthorize]
    public class SettingController : Controller
    {


        #region Ctor
        private readonly ISetting clsSetting;
        public SettingController(ISetting oSetting)
        {
            clsSetting = oSetting;
        }

        #endregion


        #region Edit
        public IActionResult Edit()
        {
            var setting = clsSetting.GetFirst();

            return View(setting);
        }
        #endregion


        #region Save

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbSettings model, List<IFormFile> Files, List<IFormFile> shopPageTopSliderImage, List<IFormFile> ShopPageLeftDownSliderImage)
        {

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }


            model.Logo = await ClsUploadImage.UploadImage(Files, "Website");
            model.ShopPageLeftDownSliderImage = await ClsUploadImage.UploadImage(ShopPageLeftDownSliderImage, "Sliders");
            model.ShopPageTopSliderImage = await ClsUploadImage.UploadImage(shopPageTopSliderImage, "Sliders");

            await clsSetting.Save(model);

            return RedirectToAction("Edit");
        }

        #endregion

    }
}
