using Domains.Models;

namespace LapShopProject.Models
{
    public class VwMyOrder
    {
        public int OrderId { get; set; }

        public string CountryName { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public DateTime OrderDate { get; set; } 
        public ICollection<TbItem> lstItems { get; set; } = new List<TbItem>();


    }
}
