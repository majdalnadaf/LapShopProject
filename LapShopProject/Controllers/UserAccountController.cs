using LapShopProject.Models;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LapShopProject.Filters;
namespace LapShopProject.Controllers
{
   
    public class UserAccountController : Controller
    {
        #region Ctor
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;

        public UserAccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {

            if (returnUrl!=null && returnUrl.Contains("Admin"))
            {

                return Redirect("/Admin/User/Login");
            }



            var vwUserModel = new VwUserRegister();
            vwUserModel.FirstName = "for skiped validation";
            vwUserModel.LastName = "for skiped validation";
            vwUserModel.ReturnUrl = returnUrl;
            return View(vwUserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> Login(VwUserRegister model)
        {



            if (!ModelState.IsValid)
                return View("Login", model);


            ApplicationUser user = new ApplicationUser();
            {
                user.Email = model.Email;
            }



            try
            {

                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password,true, true);


                if (loginResult.Succeeded)
                {


               

                    if (!loginResult.IsLockedOut)
                    {
                        // redirect to pages system depend on authrizations

                        if (string.IsNullOrEmpty(model.ReturnUrl))
                            return Redirect("/Home/Index");


                        return Redirect(model.ReturnUrl);

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
            catch
            {
                return View("Login", model);
            }


        }
        #endregion


        #region Register
        [AllowAnonymous]
        public IActionResult Register(string returnUrl)
        {
            VwUserRegister vwUserModel = new VwUserRegister();
            vwUserModel.ReturnUrl = returnUrl;
            return View(vwUserModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Save(VwUserRegister model)
        {

            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }

            ApplicationUser user = new ApplicationUser();
            {
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.Email;

            }


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                //For Role
                var myUser = await _userManager.FindByEmailAsync(user.Email);
                if (myUser != null)
                    await _userManager.AddToRoleAsync(myUser, "Customer");  // you can send the role from settings 
                                                                   




                TempData["SuccessMessage"] = "Your registration was successful!";
                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, true);
                if (loginResult.Succeeded)
                {






                    // move to specific views
                    if (model.ReturnUrl == null)
                        model.ReturnUrl = @"/Home/Index";




                      return Redirect(model.ReturnUrl);


                }

                return View("Register", model);

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


        #region Logout
        [CustomAuthorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }

        #endregion

        #region Denied page
        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }
        #endregion


        #region MyAccount
        [CustomAuthorize]
        public async Task<IActionResult> MyAccount()
        {
            ApplicationUser currentUser = new ApplicationUser();
            if (User.Identity.IsAuthenticated)
                currentUser = await _signInManager.UserManager.GetUserAsync(User);

            VwUser vwUser = new VwUser();
            vwUser.FirstName = currentUser.FirstName;
            vwUser.LastName = currentUser.LastName;
            vwUser.Email = currentUser.Email;
            vwUser.Phone = currentUser.PhoneNumber;


            return View(vwUser);
        }
        #endregion

        #region UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]

        public async Task<IActionResult> UpdateProfile(VwUser vwUser)
        {

            if (!ModelState.IsValid)
            {
                return View("MyAccount", vwUser);
            }
            // Access the current user
            var currentUser = new ApplicationUser();
            if (User.Identity.IsAuthenticated)
                currentUser = await _signInManager.UserManager.GetUserAsync(User);


            //Update the properties of current user
            currentUser.Email = vwUser.Email;
            currentUser.FirstName = vwUser.FirstName;
            currentUser.LastName = vwUser.LastName;
            currentUser.PhoneNumber = vwUser.Phone;

            //Update current user in database
            var updateResult = await _userManager.UpdateAsync(currentUser);
            if (updateResult.Succeeded)
            {

                // Build the view model with new data 
                VwUser updatedVwUser = new VwUser();
                updatedVwUser.FirstName = currentUser.FirstName;
                updatedVwUser.LastName = currentUser.LastName;
                updatedVwUser.Email = currentUser.Email;
                updatedVwUser.Phone = currentUser.PhoneNumber;

                // Send the new view model to view
                return View("MyAccount", updatedVwUser);
            }
            else
            {
                //Handle errors 
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View("MyAccount", vwUser);
            }

        } 
        #endregion

    }
}
