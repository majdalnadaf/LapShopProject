

using Domains;
using Domains.Models;

namespace LapShopProject.Models
{
    public class VwHomePage
    { 
        public VwHomePage()
        {
            lstItems = new List<TbItem>();
            lstFreeDeliveryItems = new List<TbItem>();
            lstRecommendedItems = new List<TbItem>();
            TbSettings = new TbSettings();
            lstSliders = new List<TbSlider>();    
        }

        public List<TbSlider> lstSliders { get; set; }
        public List<TbItem> lstItems { get; set; }
        public List<TbItem> lstFreeDeliveryItems { get; set; }
        public List<TbItem> lstRecommendedItems { get; set; }
        public TbSettings TbSettings { get; set; }


    }
}
