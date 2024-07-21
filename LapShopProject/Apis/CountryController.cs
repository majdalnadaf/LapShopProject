using Microsoft.AspNetCore.Mvc;
using DataAccess.DaModels.Interfaces;
using DataAccess.DaModels;
using Domains.Models;
using LapShopProject.Models;
using System.Diagnostics.Metrics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShopProject.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        ICountry clsCountry;
        public CountryController(ICountry oCountry)
        {
            clsCountry = oCountry;
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetById(int id)
        {

            var response = new ApiResponse();
            response.Data = new TbCountry();

            try
            {
                var country = await clsCountry.GetById(id);
                if (country != null)
                    response.Data = country;
                 
     
                
                response.Errors = null;
                response.CodeStatus = "200";

                return response;

            }
            catch (Exception e)
            {

                response.Data = null;
                response.Errors = e.Message + "";
                response.CodeStatus = "502";


                return response;
                throw new Exception();
            }

        }


    }
}
