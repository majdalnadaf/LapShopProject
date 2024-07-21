using System.ComponentModel.DataAnnotations;

namespace LapShopProject.Models
{
    public class VwReview
    {
        [Required]
        [StringLength(200)]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(300)]
        public string ReviewTitle { get; set; }
        [Required]
        [StringLength(2000)]
        public string ReviewDescription { get; set;}

        [Range(1,5)]
        public int Rating { get; set; }
    }
}
