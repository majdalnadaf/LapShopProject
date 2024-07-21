using DataAccess;
using DataAccess.DaModels.Interfaces;
using Domains.Models.VwAdmin;
using Domains.Models;
using LapShopProject.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.BlHelper.BlAdmin;
using BusinessLogic.BlHelper.BlWebsite;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BusinessLogic.Bl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LapShopProject.Controllers
{
    public class ItemDetailsController : Controller
    {
        #region Ctor
        private readonly IVwItem clsVwItem;
        private readonly IItem clsItem;
        private readonly IItemImage clsItemImage;
        private readonly IItemDiscount clsItemDiscount;
        private readonly IReview clsReview;
        private readonly ICustomer clsCustomer;


        public ItemDetailsController(IVwItem oVwItem, IItem oItem, IItemImage oItemImage, IItemDiscount oItemDiscount, IReview oReview, ICustomer oCustomer)
        {
            clsVwItem = oVwItem;
            clsItem = oItem;
            clsItemImage = oItemImage;
            clsItemDiscount = oItemDiscount;
            clsReview = oReview;
            clsCustomer = oCustomer;

        }

        #endregion

        #region Details
        [AllowAnonymous]
        public IActionResult Details( int itemId)
        {
            VwItemDetails vw = new VwItemDetails();
            vw.ItemId = itemId;
            vw.Item = clsVwItem.GetById(itemId);
            vw.lstRelatedItems = ClsRelatedItems.GetFreeDeliveryItems(clsVwItem, vw.Item).Take(12).ToList();
            vw.lstItemImage = clsItemImage.GetAll(itemId);
            vw.ItemDiscount = clsItemDiscount.GetById(itemId);

            return View(vw);
        }
        #endregion

        #region Save Review


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult SaveReview(VwItemDetails itemDetails)
        {


            if (!TryValidateModel(itemDetails.VwReview, nameof(itemDetails.VwReview)))
            {

                return View("Details", itemDetails);
            }



            var oCustomer = new TbCustomer()
            {
                CustomerEmail = itemDetails.VwReview.CustomerEmail,
                CustomerName = itemDetails.VwReview.CustomerName
            };

            int customerId;
            clsCustomer.Save(oCustomer, out customerId);


            var oReview = new TbReview()
            {
                ItemId = itemDetails.ItemId,
                CustomerId = customerId,
                Rating = itemDetails.VwReview.Rating,
                ReviewTitle = itemDetails.VwReview.ReviewTitle,
                ReviewDescription = itemDetails.VwReview.ReviewDescription
            };

            clsReview.Save(oReview);


            return RedirectToAction("Details", new { itemDetails.ItemId});

        }
        #endregion
    }
}
