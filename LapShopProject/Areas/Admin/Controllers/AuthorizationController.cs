using DataAccess.Identity;
using LapShopProject.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LapShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [CustomAuthorize]
    public class AuthorizationController : Controller
    {
        #region Ctor
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizationController(RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        #endregion


        #region Edit
        public async Task<IActionResult> Edit()
        {

            List<ApplicationRole> lstRoles = _roleManager.Roles.ToList();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);


                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    lstRoles = lstRoles.Where(a => (a.Name != "Manager" && a.Name != "Admin")).ToList();
                }
            }


            ViewBag.RoleList = lstRoles;
            return View();
        } 
        #endregion
    }
}
