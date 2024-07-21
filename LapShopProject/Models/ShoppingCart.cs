namespace LapShopProject.Models
{
    public class ShoppingCart
    {

        public ShoppingCart() 
        {
            lstItem = new List<ShoppingCartItem>(); 
        }
        public List<ShoppingCartItem> lstItem { get; set; }
        public string PromoCode { get; set; } = string.Empty;   

        public decimal TotalPriceOfAllItems { get; set; }  



    }
}
