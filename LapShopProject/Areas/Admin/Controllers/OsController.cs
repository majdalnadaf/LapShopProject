using Microsoft.AspNetCore.Mvc;
using Domains.Models;
using DataAccess.DaModels.Interfaces;
using BusinessLogic.BlHelper.BlAdmin;
using Microsoft.AspNetCore.Identity;
using DataAccess.Identity;
using LapShopProject.Filters;

namespace LapShopProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [CustomAuthorize]
    public class OsController : Controller
    {
        #region Ctor
        private readonly IOs clsOs;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public OsController(IOs oOs, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            clsOs = oOs;
            _userManager = userManager;
            _signInManager = signInManager;
        } 
        #endregion

        #region List
        public async Task<IActionResult> List()
        {

            List<TbOs> lstOs = await clsOs.GetAll();
            return View(lstOs);

        } 
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int osId)
        {

            var os = new TbOs();

            if (osId != 0)
                os = await clsOs.GetById(osId);

            return View(os);
        } 
        #endregion

        #region Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbOs model, List<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            if (Files.Count > 0)
                model.ImageName = await ClsUploadImage.UploadImage(Files, "Os");
            else
            {
                model.ImageName = string.Empty;
            }

            model.CreatedDate = DateTime.Now;

            var currentUser = await _signInManager.UserManager.GetUserAsync(User);
            if (currentUser != null)
                model.CreatedBy = currentUser.Id;
            else
            {
                model.CreatedBy = "";
            }


            if (!await clsOs.Save(model))
                return View("Edit", model);


            return RedirectToAction("List");
        } 
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int osId)
        {
            if (osId != 0)
            {
                await clsOs.Delete(osId);
            }

            return RedirectToAction("List");
        } 
        #endregion

    }
}
