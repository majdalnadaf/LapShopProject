using Domains.Models;
using LapShopProject.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LapShopProject.Controllers
{
    public class InfoController : Controller
    {
        #region Contact

        [CustomAuthorize]
        public IActionResult Contact()
        {
            ViewBag.location = new TbLocation();

            return View(new TbContactInfo());
        } 
        #endregion
    }
}
