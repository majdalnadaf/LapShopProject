using DataAccess.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.BlHelper;
using LapShopProject.Apis.BlHelper;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShopProject.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public LoginController(IConfiguration config)
        {
            configuration = config;
        }


        /// <summary>
        /// Validate user and returns token
        /// </summary>
        /// <param name="user"></param>
        /// 
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] ApplicationUser user)
        {
            var response = Unauthorized();

            var myUser = ClsAuthorizationApi.AuthorizeUser(user);
            if (myUser != null) 
            {
                ClsGenerateToken oGenerateToken = new ClsGenerateToken(configuration);
                var token = oGenerateToken.GenerateToken();
                return  Ok(new { token = token });

            }

            return response;
        }

    }
}
