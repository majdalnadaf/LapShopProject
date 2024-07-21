using DataAccess.DaModels.Interfaces;
using Domains.Models;
using LapShopProject.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LapShopProject.Controllers
{

    [CustomAuthorize]
    public class UserWishListController : Controller
    {
        #region Ctor
        private readonly IItem clsItem;
        public UserWishListController(IItem oItem)
        {
            clsItem = oItem;
        }
        #endregion



        #region WishList
        public IActionResult WishList()
        {

            var lstWishListItem = new List<TbItem>();

            var jsonWishList = HttpContext.Request.Cookies["WishList"];
            if (jsonWishList != null)
            {
                lstWishListItem = JsonConvert.DeserializeObject<List<TbItem>>(jsonWishList);
            }

            return View(lstWishListItem);
        }


        public async Task<IActionResult> AddToWishList(int itemId)
        {

            var item = await clsItem.GetById(itemId);

            var lstWishListItem = new List<TbItem>();

            var jsonWishList = HttpContext.Request.Cookies["WishList"];
            if (jsonWishList != null)
            {
                lstWishListItem = JsonConvert.DeserializeObject<List<TbItem>>(jsonWishList);
            }

            lstWishListItem.Add(item);

            var newJsonWishList = JsonConvert.SerializeObject(lstWishListItem);

            HttpContext.Response.Cookies.Append("WishList", newJsonWishList);

            return RedirectToAction("WishList");
        } 
        #endregion

    }
}
