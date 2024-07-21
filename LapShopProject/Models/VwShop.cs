
using Domains.Models;

namespace LapShopProject.Models
{
    public class VwShop
    {
        public VwShop()
        {
            lstCategory = new List<TbCategory>();
            Setting = new TbSettings();
            lstNewItem = new List<TbItem>();    
        }

        public TbSettings Setting { get; set; }
        public List<TbCategory> lstCategory { get; set; }

        public List<TbItem> lstNewItem { get; set; }    
    }
}
