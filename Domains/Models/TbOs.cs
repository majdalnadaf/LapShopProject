using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains.Models;


public partial class TbOs
{
    public int OsId { get; set; }

    [Required]
    [StringLength(200)]
    public string OsName { get; set; } = string.Empty;

    [ValidateNever]
    public string ImageName { get; set; } = string.Empty;

    public bool ShowInHomePage { get; set; }

    [ValidateNever]
    public int CurrentState { get; set; }

    [ValidateNever]

    public DateTime CreatedDate { get; set; }

    [ValidateNever]

    public string CreatedBy { get; set; } = string.Empty;
    [ValidateNever]


    public DateTime? UpdatedDate { get; set; }
    [ValidateNever]


    public string? UpdatedBy { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
