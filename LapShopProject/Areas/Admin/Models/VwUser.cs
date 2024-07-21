using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LapShopProject.Areas.Admin.Models
{
    public class VwUser
    {
        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
