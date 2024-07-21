
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LapShopProject.Models
{
    public class VwUserRegister
    {

        public VwUserRegister()
        {
  
        }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [PasswordPropertyText]
        [Required]
        public string Password { get; set; } = string.Empty;

        [ValidateNever]
        public string ReturnUrl { get; set; } = string.Empty;

        [ValidateNever]
        public string Role { get; set; } = string.Empty;
    }
}
