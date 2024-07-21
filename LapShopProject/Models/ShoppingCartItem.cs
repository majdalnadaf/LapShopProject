using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShopProject.Models
{
    public class ShoppingCartItem
    {
        public int ItemId {get; set;}
        public string ItemName { get; set;} = string.Empty;

        public string ImageName { get; set;} = string.Empty;

        public decimal Price { get; set;}

        [ValidateNever]
        public float Qty { get; set;}   

        public decimal TotalPrice { get; set;}


    }
}
