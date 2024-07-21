using System.ComponentModel.DataAnnotations;

namespace LapShopProject.Models
{
    public class VwUser
    {
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;
    }
}
