using LapShopProject.Models;
using DataAccess;
using Domains.Models;
using Domains.Models.VwAdmin;
using BusinessLogic.Bl;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.BlHelper.BlAdmin;
using DataAccess.DaModels.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LapShopProject.Controllers
{
    public class HomeController : Controller
    {
        #region Ctor

        private readonly IItem clsItem;
        private readonly ISetting clsSetting;
        private readonly ISlider clsSlider;
        private readonly ICategory clsCategory;
        private readonly IPage clsPage; 

        public HomeController(IItem oitem, ISetting oSetting, ISlider oSlider, ICategory oCategory , IPage oPage)
        {
            clsItem = oitem;
            clsSetting = oSetting;
            clsSlider = oSlider;
            clsCategory = oCategory;
            clsPage = oPage;
        }
        #endregion

        #region Home
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            VwHomePage vw = new VwHomePage();
            vw.lstItems = (await clsItem.GetAll()).Take(18).ToList();
            vw.lstRecommendedItems = (await ClsRecommendedItem.GetRecommendedItems(clsItem)).Take(8).ToList();      // there is logic for this list it goes to database directly
            vw.lstFreeDeliveryItems = (await ClsFreeDeliveryItems.GetFreeDeliveryItems(clsItem)).Take(8).ToList();  // there is logic for this list it goes to database directly
            vw.TbSettings = clsSetting.GetFirst();
            vw.lstSliders = (await clsSlider.GetAll()).Take(4).ToList();
            return View(vw);
        }
        #endregion

        #region Shop
        [AllowAnonymous]

        public async Task<IActionResult> Shop()
        {
            VwShop oVwShop = new VwShop();
            oVwShop.lstCategory = await clsCategory.GetAll();
            oVwShop.Setting = clsSetting.GetFirst();
            oVwShop.lstNewItem = (await clsItem.GetAll()).OrderByDescending(a => a.CreatedDate).Take(6).ToList(); // number 6 from tbsetting in database for more control
            return View(oVwShop);
        }
        #endregion

        #region DisplayAdditionalPage
        [AllowAnonymous]

        public IActionResult DisplayAdditionalPage(int pageId)
        {

            var oAdditionalPage = clsPage.GetById(pageId);

            return View(oAdditionalPage);
        } 
        #endregion

        public IActionResult Error() 
        {
            return View();
        }



    }
}
