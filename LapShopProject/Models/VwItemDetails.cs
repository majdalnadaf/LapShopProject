
using Domains;
using Domains.Models;
using Domains.Models.VwAdmin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShopProject.Models 
{
 
    public class VwItemDetails
    {
        public VwItemDetails()
        {
            Item = new VwItem();
            ItemDiscount = new TbItemDiscount();
            lstRelatedItems = new List<VwItem>();
            lstItemImage = new List<TbItemImage>();
            Settings = new TbSettings();
            VwReview = new VwReview();
        }

        [ValidateNever]
        public int ItemId { get; set; }

        [ValidateNever]
        public VwItem Item { get; set; }

        [ValidateNever]

        public List<TbItemImage> lstItemImage { get; set; }
        [ValidateNever]

        public List<VwItem> lstRelatedItems { get; set; }
        [ValidateNever]

        public TbItemDiscount ItemDiscount { get; set; }

        public VwReview VwReview { get; set; }


        [ValidateNever]

        public TbSettings Settings { get; set; }

    }
}
