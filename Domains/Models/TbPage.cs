using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Models
{
    public class TbPage
    {
        public int PageId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public bool ShowInHomePage { get; set; }

        [Required]
     
        public string HtmlContent { get; set; } = string.Empty;

        [ValidateNever]
        
        public string CssContent {  get; set; } = string.Empty;

        [ValidateNever]

        public string JsContent { get; set; } = string.Empty;

        [ValidateNever]

        public string MetaTagsHtml { get; set; } = string.Empty;


    }
}
