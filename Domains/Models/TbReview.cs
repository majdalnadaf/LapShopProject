using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    
    public class TbReview
    {
        public TbReview()
        {

        }

        [ValidateNever]
        public int ReviewId { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }

        [StringLength(100)]
        [Required]
        public string ReviewTitle { get; set; } = string.Empty;
        [Required]
        public string ReviewDescription { get; set;} = string.Empty;

        [ValidateNever]
        public int ItemId { get; set; }

        [ValidateNever]
        public virtual  TbItem item { get; set; }

        [ValidateNever]
        public int CustomerId { get; set; }

        [ValidateNever]
        public virtual TbCustomer Customer { get; set; }



    }
}
