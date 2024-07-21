using DataAccess;
using DataAccess.Identity;
using LapShopProject.Areas.Admin.Models;
using LapShopProject.Models;
using Domains.Models.SpAdmin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.DaModels.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;


namespace LapShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        #region Ctor

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DbLapShopContext context;
        private readonly IUserRole clsUserRole;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager,
            DbLapShopContext ctx, IUserRole osUserRole)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            context = ctx;
            clsUserRole = osUserRole;
        } 
        #endregion


        #region Register
        public async Task<IActionResult> Register()
        {
            VwUserRegister oVwUserRegister = new VwUserRegister();


            ViewBag.RoleList = await GetAllRole();

            return View(oVwUserRegister);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(VwUserRegister model, string roleId)
        {

            ViewBag.RoleList = await GetAllRole();


            if (!ModelState.IsValid)
            {

                return View("Register", model);
            }


            // Save new user



            ApplicationUser user = new ApplicationUser();
            {
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.Email;

            }


            var result = await _userManager.CreateAsync(user, model.Password);

            //Check the create new user is successed or not

            if (result.Succeeded)
            {

                //For Role
                var myUser = await _userManager.FindByEmailAsync(user.Email);
                var sRoleName = _roleManager.Roles.ToList().Where(a => a.Id == roleId).FirstOrDefault();


                if (myUser != null && sRoleName != null)
                    await _userManager.AddToRoleAsync(myUser, sRoleName.Name);

                TempData["SuccessMessage"] = "Your registration was successful!";

                return Redirect("/Admin/User/Register");

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }


                return View("Register", model);
            }



        }
        #endregion


        #region List
        public async Task<IActionResult> List()
        {

            // Get all UserRole from database

            List<SpUserRole> lstUserRole = context.SpUserRoles.FromSqlRaw("EXEC usp_SPUserRole").ToList();
            ViewBag.lstError = new List<string>();
            ViewBag.RoleList = await GetAllRole();


            return View(lstUserRole);
        }
        #endregion


        #region Delete
        public async Task<IActionResult> Delete(string userId)
        {
            //Delete logic


            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                var lstError = new List<string>();
                lstError.Add("User not found ");


                ViewBag.RoleList = await GetAllRole();
                ViewBag.lstError = lstError;
                List<SpUserRole> lstUserRole = context.SpUserRoles.FromSqlRaw("EXEC usp_SPUserRole").ToList();

                return View("List",lstUserRole);

            }

            var lstRoleOfUser = await _userManager.GetRolesAsync(user);

            for(int i = 0; i < lstRoleOfUser.Count; i++)
            {
                await _userManager.RemoveFromRoleAsync(user, lstRoleOfUser[i]);
            }


            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {

                var lstError = new List<string>();
                
                // Add errors to lstError

                foreach (var error in deleteResult.Errors)
                {
                    lstError.Add(error.Description);
                }

                ViewBag.RoleList = await GetAllRole();
                ViewBag.lstError = lstError;
                List<SpUserRole> lstUserRole = context.SpUserRoles.FromSqlRaw("EXEC usp_SPUserRole").ToList();
                
                return View("List", lstUserRole);



            }


            return RedirectToAction("List");
        }
        #endregion

        #region Edit

        public async Task<IActionResult> Edit(string userId)
        {
            VwUserRegister oVwUserRegister = new VwUserRegister();

            if (!string.IsNullOrEmpty(userId))
            {
                // get user by userid from database

                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    oVwUserRegister.FirstName = user.FirstName;
                    oVwUserRegister.LastName = user.LastName;
                    oVwUserRegister.Email = user.Email;

                    var userRole = clsUserRole.GetById(userId, true);
                    if (userRole != null)
                        oVwUserRegister.Role = userRole.RoleId;
                }

            }

            ViewBag.UserId = userId;



            ViewBag.RoleList = await GetAllRole();

            return View(oVwUserRegister);
        }


        public async Task<IActionResult> Update(VwUserRegister model, string userId, string roleId , string oldPassword)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.RoleList = await GetAllRole();
                return View("Register", model);
            }


            // Get user form database

            var existsUser = await _userManager.FindByIdAsync(userId);
            if (existsUser != null)
            {// Update exists user info

                existsUser.FirstName = model.FirstName;
                existsUser.LastName = model.LastName;
                existsUser.Email = model.Email;

                //Get users roles from database

                var oldUserRoles = await _userManager.GetRolesAsync(existsUser);
                if (oldUserRoles.Count != 0)
                {
                    // To Update users role first remove the old role then add the new role 

                    // Remove old role

                    await _userManager.RemoveFromRoleAsync(existsUser, oldUserRoles[0]);

                }



                // Get the new role by id

                var newRole = await _roleManager.FindByIdAsync(roleId);
                //Add new role
                if (newRole != null)
                    await _userManager.AddToRoleAsync(existsUser, newRole.Name);


                // ---------------------->

                // Verify the original password
                var isOriginalPasswordValid = await _userManager.CheckPasswordAsync(existsUser,oldPassword );
                if (!isOriginalPasswordValid)
                {
                    // Handle invalid original password (e.g., show an error message)

                    ModelState.AddModelError(string.Empty, "Incorrect password");
                

                    ViewBag.RoleList = await GetAllRole();
                    return View("Edit", model);

            }

                // Generate a new hashed password
                var newPasswordHash = _userManager.PasswordHasher.HashPassword(existsUser, model.Password);

                // Update the user's password
                existsUser.PasswordHash = newPasswordHash;

                //----------------------->


                var result = await _userManager.UpdateAsync(existsUser);
                if (!result.Succeeded)
                {

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }



                    return View("Edite", model);

                }


            }
            else
            {
                // Not found the user by id

                ViewBag.RoleList = await GetAllRole();
                ModelState.AddModelError(string.Empty, "The user is not found");
                return View("Edit", model);
            }


            return RedirectToAction("List");
        }
        #endregion


        #region Login
        [AllowAnonymous]
        public IActionResult Login()
        {
            LapShopProject.Areas.Admin.Models.VwUser oVwUser = new Models.VwUser();
            return View(oVwUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(Models.VwUser model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }


            try
            {

                var loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
                if (loginResult.Succeeded)
                {


                    if (!loginResult.IsLockedOut)
                    {
                        // redirect to pages system depend on authrizations


                        return Redirect("/Admin/Home/Dashbord");

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Your account is locked out ,Try again later.");
                        return View("Login", model);

                    }


                }
                else
                {
                    ModelState.AddModelError(string.Empty, "We couldn't find an account matching that username and password. Please double-check your credentials.");
                    return View("Login", model);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        } 
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
        #endregion

        #region Denied
        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }
        #endregion

        #region Custom Function
        [AllowAnonymous]
        async Task<List<ApplicationRole>> GetAllRole()
        {
            // for role list
            List<ApplicationRole> lstRoles = _roleManager.Roles.ToList();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var oUserSingIn = await _userManager.GetUserAsync(User);


                if (await _userManager.IsInRoleAsync(oUserSingIn, "Admin"))
                {
                    lstRoles = lstRoles.Where(a => (a.Name != "Manager" && a.Name != "Admin")).ToList();
                }
            }
            else
            {
                // lstRoles = new List<ApplicationRole>();
            }

            return lstRoles;
        } 
        #endregion

    }
}
