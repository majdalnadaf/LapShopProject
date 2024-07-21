using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Models;



public partial class TbCustomer
{

    [ValidateNever]
    public int CustomerId { get; set; }

    [StringLength(100)]
    [Required]
    public string CustomerName { get; set; } = string.Empty!;
    [Required]
    [EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;

    [ValidateNever]
    public virtual TbBusinessInfo? TbBusinessInfo { get; set; }
    [ValidateNever]
    public virtual ICollection<TbItem> Items { get; set; } = new List<TbItem>();
    [ValidateNever]
    public virtual ICollection<TbReview> Reviews { get; set; } = new HashSet<TbReview>();


}
