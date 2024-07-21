using System;
using System.Collections.Generic;

namespace Domains.Models;


public partial class TbSlider
{
    public int SliderId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageName { get; set; } = string.Empty;

    public bool HomeSlider { get; set; }

    public bool CollectionBanner { get; set; }

    public bool InstegramSection { get; set; }

    public string InstegramLink { get; set; } = string.Empty;   

    public bool AdvertisementBanner { get; set; }   
    public string AdvertismenetLink { get; set; } = string.Empty;
}
