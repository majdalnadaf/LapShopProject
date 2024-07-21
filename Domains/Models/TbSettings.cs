using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Domains.Models
{


    public class TbSettings
    {
        [Key]
        public int SettingId { get; set; }
        [StringLength(400)]
        public string Location { get; set; } = string.Empty;
        [StringLength(1000)]
        public string HeaderStatement { get; set; } = string.Empty;
        [StringLength(400)]
        public string Email { get; set; } = string.Empty;
        [StringLength(50)]
        public string NumberCallUs { get; set; } = string.Empty;


        [ValidateNever]

        public bool AdvertisementBanner { get; set; }
        [ValidateNever]

        public string AdvertismenetLink { get; set; } = string.Empty;
        [ValidateNever]

        public string ParagraphTitle { get; set; } = string.Empty;
        [ValidateNever]

        public string ParagraphDescription { get; set; } = string.Empty;
        [ValidateNever]

        public string ParagraphSubTitle { get; set; } = string.Empty;
        [ValidateNever]

        public string ParallaxBannerTitle { get; set; } = string.Empty;
        [ValidateNever]

        public string ParallaxBannerSubTitle { get; set; } = string.Empty;
        [ValidateNever]

        public string ParallaxBannerDescription { get; set; } = string.Empty;
        [ValidateNever]

        public string Logo { get; set; } = string.Empty;
        [ValidateNever]

        public string Title { get; set; } = string.Empty;
        [ValidateNever]

        public string Description { get; set; } = string.Empty;
        [ValidateNever]

        public string YoutubeLink { get; set; } = string.Empty;
        [ValidateNever]

        public string FacebookLink { get; set; } = string.Empty;
        [ValidateNever]

        public string TwitterLink { get; set; } = string.Empty;
        [ValidateNever]

        public string InstgramLink { get; set; } = string.Empty;

        [ValidateNever]

        public string ShopPageTopSliderImage { get; set; } = string.Empty;
        [ValidateNever]

        public string ShopPageTopSliderLink { get; set; } = string.Empty;

        [ValidateNever]

        public string ShopPageTopSliderTitle { get; set; } = string.Empty;

        [ValidateNever]

        public string ShopPageTopSliderDescription { get; set; } = string.Empty;

        [ValidateNever]

        public string ShopPageLeftDownSliderImage { get; set; } = string.Empty;
        [ValidateNever]

        public string ShopPageLeftDownSliderLink { get; set; } = string.Empty;


    }
}
