using DataAccess.DaModels.Interfaces;
using Domains.Models;
using LapShopProject.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LapShopProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [CustomAuthorize]
    public class CountryController : Controller
    {

        #region Ctor
        private readonly ICountry clsCountry;

        public CountryController(ICountry OCountry)
        {
            clsCountry = OCountry;
        }
        #endregion


        #region List

        public async Task<IActionResult> List()
        {

            var lstCounstry = await clsCountry.GetAll();

            return View(lstCounstry);
        }

        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int countryId)
        {

            var country = new TbCountry();
            if (countryId != 0)
            {
                country = await clsCountry.GetById(countryId);
            }

            return View(country);
        }
        #endregion

        #region Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(TbCountry model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            if (!await clsCountry.Save(model))
            {
                return View("Edit", model);
            }


            return RedirectToAction("List");

        }
        #endregion

        #region Delete
        public IActionResult Delete(int countryId)
        {
            if (countryId != 0)
            {
                clsCountry.Delete(countryId);
            }

            return RedirectToAction("List");
        } 
        #endregion
    }
}
