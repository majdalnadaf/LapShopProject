using LapShopProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShopProject.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
 

        // POST api/<OrderController>
        /// <summary>
        /// Update Cart Cookie
        /// </summary>
        /// <param name="cart"></param>
        [HttpPost]
        [Route("UpdateCart")]
        public IActionResult UpdateCart([FromBody] ShoppingCart cart)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                // Start : Validate the ShoppingIems if any one of them has negative quantity 


                for (int i = 0; i < cart.lstItem.Count;i++)
                {
                    if (cart.lstItem[i].Qty <= 0) 
                    {
                        cart.lstItem.Remove(cart.lstItem[i]);
                    }
                }

                // End : Validate the ShoppingIems if any one of them has negative quantity 

                var jsonCart = JsonConvert.SerializeObject(cart);
                HttpContext.Response.Cookies.Append("Cart", jsonCart);

                return Ok();
            }
            catch (Exception e )
            {

                throw new Exception();
            }
        }


    }
}
