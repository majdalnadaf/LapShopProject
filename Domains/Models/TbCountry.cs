using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Domains.Models
{
    public class TbCountry
    {

        public TbCountry()
        {
            TbDeliveriesInfo  = new List<TbDeliveryInfo>();
        }

        public int CountryId { get; set; }

        [Required]
        public string CountryName { get; set; } = string.Empty;

        [ValidateNever]
        public string CountryCode { get; set; } = string.Empty;


        [Required]
        public decimal CostDelivery { get; set; }

        public virtual ICollection<TbDeliveryInfo> TbDeliveriesInfo { get; set; } 

    }
}
