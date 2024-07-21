using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LapShopProject.Apis.BlHelper
{
    public class ClsGenerateToken
    {
        private readonly IConfiguration configuration;
        public ClsGenerateToken(IConfiguration config)
        {
                configuration = config; 
        }

        public string GenerateToken() 
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

           return new JwtSecurityTokenHandler().WriteToken(token);
           
        }
    }
}
