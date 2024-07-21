using LapShopProject.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LapShopProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [CustomAuthorize]

    public class HomeController : Controller
    {
        
        public IActionResult Dashbord()
        {
            return View();
        }
    }
}
