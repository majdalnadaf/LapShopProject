using DataAccess.ApisCalling;
using DataAccess.DaModels.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Domains.Models;
using Newtonsoft.Json;
using LapShopProject.Filters;
using LapShopProject.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShopProject.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWishListController : ControllerBase
    {
        
        public UserWishListController()
        {
                 
        }


        // POST api/<UserWishListController>
        [HttpPost]
        public IActionResult UpdateWishList([FromBody] ApiUserWishListModel model)
        {
            try
            {


                if (model.ItemId == 0)
                {
                    return BadRequest(new { error = "Not found item" });
                }

                // when item id is valid

                var lstWishListItem = new List<TbItem>();

                var jsonWishList = HttpContext.Request.Cookies["WishList"];
                if (jsonWishList != null)
                {
                    lstWishListItem = JsonConvert.DeserializeObject<List<TbItem>>(jsonWishList);
                }

                var item = lstWishListItem.Where(a=> a.ItemId==model.ItemId).FirstOrDefault();
              
                // item dosent exist in the wishlist
                if(item==null)
                    return BadRequest(new { error = "Not found item in wish list" });

                lstWishListItem.Remove(item);

                var newJsonWishList = JsonConvert.SerializeObject(lstWishListItem);

                HttpContext.Response.Cookies.Append("WishList", newJsonWishList);

                return Ok(new { message = "Wish list updated" });


            }
            catch (Exception ex)
            {
                return BadRequest();
                throw new Exception();
            }
        }


    }
}
