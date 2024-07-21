using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace DataAccess.Identity
{
    public class ApplicationRole : IdentityRole
    {
    
        public string WebsiteRoleActions { get; set; } = @"
{
    ""Home"": {
        ""Index"": false,
        ""Shop"": false
    },
    ""Info"": {
        ""Contact"": false
    },
    ""ItemDetails"": {
        ""Details"": false,
        ""SaveReview"": false
    },
    ""Order"": {
        ""OrderSuccess"": false,
        ""CheckOut"": false,
        ""SaveCheckOut"": false,
        ""Cart"": false,
        ""AddToCart"": false,
        ""MyOrders"": false
    },
    ""UserAccount"": {
        ""Login"": false,
        ""Register"": false,
        ""Save"": false,
        ""Logout"": false,
        ""Denied"": false,
        ""MyAccount"": false,
        ""UpdateProfile"": false
    },
    ""UserWishList"": {
        ""WishList"": false,
        ""AddToWishList"": false
    }
}";
        public string AdminRoleActions { get; set; } = @"
{
    ""Home"": {
        ""Index"": false,
        ""Shop"": false
    },
    ""Info"": {
        ""Contact"": false
    },
    ""ItemDetails"": {
        ""Details"": false,
        ""SaveReview"": false
    },
    ""Order"": {
        ""OrderSuccess"": false,
        ""CheckOut"": false,
        ""SaveCheckOut"": false,
        ""Cart"": false,
        ""AddToCart"": false,
        ""MyOrders"": false
    },
    ""UserAccount"": {
        ""Login"": false,
        ""Register"": false,
        ""Save"": false,
        ""Logout"": false,
        ""Denied"": false,
        ""MyAccount"": false,
        ""UpdateProfile"": false
    },
    ""UserWishList"": {
        ""WishList"": false,
        ""AddToWishList"": false
    }
}";

    }
}
