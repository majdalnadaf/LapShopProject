using DataAccess.Identity;
using Domains.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
namespace LapShopProject.Models
{        /// <summary>
         /// For cart and user info
         /// </summary>
    public class VwCheckout
    {
        [Required]
        public int CountryId { get; set; }

        [Required]
        [StringLength(2000)]
        public string StreetAddress { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string State { get; set; } = string.Empty;

        [ValidateNever]
        public string PostalCode { get; set; } = string.Empty;
    }
}
