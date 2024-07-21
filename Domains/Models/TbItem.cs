
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Models;


public partial class TbItem
{
    [ValidateNever]
    public int ItemId { get; set; }

    [StringLength(100)]
    [Required]
    public string ItemName { get; set; } = null!;
    
    [Required]
    public decimal SalesPrice { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal PurchasePrice { get; set; }
    
    [Required]
    public int CategoryId { get; set; }

    [ValidateNever]

    public string ImageName { get; set; } = string.Empty;
  
    [ValidateNever]

    public DateTime CreatedDate { get; set; }

    [ValidateNever]

    public string CreatedBy { get; set; } = null!;

    [ValidateNever]

    public int CurrentState { get; set; }

    [ValidateNever]

    public string? UpdatedBy { get; set; }

    [ValidateNever]

    public DateTime? UpdatedDate { get; set; }

    [ValidateNever]
    public string? Description { get; set; }

    [Required]
    [StringLength(100)]
    public string? Gpu { get; set; }

    [Required]
    [StringLength(100)]

    public string? HardDisk { get; set; }

    [Required]
    public int? ItemTypeId { get; set; }

    [Required]
    [StringLength(100)]

    public string? Processor { get; set; }

    [Required]
    [Range(1,1000)]
    public int? RamSize { get; set; }

    [Required]
    [Range(1,100000)]
    public string? ScreenReslution { get; set; }

    [Required]
    [Range(1,100)]
    public string? ScreenSize { get; set; }

    [Required]
    public string? Weight { get; set; }

    [Required]
    public int? OsId { get; set; }

    [Range(1,5)]
    public int Rating { get; set; }

    [ValidateNever]

    public virtual TbCategory Category { get; set; } = null!;
    [ValidateNever]

    public virtual TbItemType? ItemType { get; set; }
    [ValidateNever]

    public virtual TbOs? Os { get; set; }
    [ValidateNever]

    public virtual ICollection<TbItemDiscount> TbItemDiscounts { get; set; } = new List<TbItemDiscount>();
    [ValidateNever]

    public virtual ICollection<TbItemImage> TbItemImages { get; set; } = new List<TbItemImage>();
    [ValidateNever]

    public virtual ICollection<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; } = new List<TbPurchaseInvoiceItem>();
    [ValidateNever]

    public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; } = new List<TbSalesInvoiceItem>();
    [ValidateNever]

    public virtual ICollection<TbCustomer> Customers { get; set; } = new List<TbCustomer>();
    [ValidateNever]

    public virtual ICollection<TbReview> Reviews { get; set; } = new HashSet<TbReview>();
}
