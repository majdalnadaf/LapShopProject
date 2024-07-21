using Microsoft.AspNetCore.Mvc;
using DataAccess.DaModels.Interfaces;
using DataAccess.DaModels;
using Domains.Models;
using LapShopProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using LapShopProject.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LapShopProject.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItem clsItem;
        public ItemController(IItem oItem)
        {

            clsItem = oItem;
        }


        // GET: api/<ItemController>
        [HttpGet]
        [IgnoreReferenceLoop]
        public async Task<ApiResponse> Get()
        {

            try
            {
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = await clsItem.GetAll();
                oApiResponse.Errors = null;
                oApiResponse.CodeStatus = "200";
                return oApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.CodeStatus = "502";
                throw new Exception();
            }

        }

        // GET api/<ItemController>/5+
        [HttpGet("{id}")]
        [IgnoreReferenceLoop]
        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = await clsItem.GetById(id);
                oApiResponse.Errors = null;
                oApiResponse.CodeStatus = "200";

                return oApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.CodeStatus = "502";
                throw new Exception();
            }
        }

        /// <summary>
        /// Save / Update Item in database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        // POST api/<ItemController>
        [HttpPost]
        [Route("post")]
        public ApiResponse Post([FromBody] TbItem item)
        {
            try
            {
                ApiResponse oApiResponse = new ApiResponse();

                if (!ModelState.IsValid)
                {
                    var validationErrors = new List<string>();
                    foreach (var modeState in ModelState.Values)
                    {
                        foreach (var error in modeState.Errors)
                        {
                            validationErrors.Add(error.ErrorMessage);
                        }
                    }

                    oApiResponse.Data = null;
                    oApiResponse.Errors = validationErrors;
                    oApiResponse.CodeStatus = "502";
                    return (oApiResponse);

                }




                oApiResponse.Data = "done";
                oApiResponse.Errors = null;
                oApiResponse.CodeStatus = "200";
                clsItem.Save(item);
                return oApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.CodeStatus = "502";
                throw new Exception();
            }
        }


        //// DELETE api/<ItemController>/5
        //[HttpPost("{id}")]
        //[Route("Delete")]
        //public ApiResponse Delete([FromBody] int id)
        //{
        //    try
        //    {    ApiResponse oApiResponse = new ApiResponse();
            
        //        if (id < 0)
        //        {
        //            oApiResponse.Data = null;
        //            oApiResponse.Errors = "isnt Valid number";
        //            oApiResponse.CodeStatus = "502";
        //            return oApiResponse;
        //        }

        //        clsItem.Delete(id);

        //        oApiResponse.Data = "done";
        //        oApiResponse.Errors = null;
        //        oApiResponse.CodeStatus = "200";

        //        return oApiResponse;
        //    }
            
        //    catch (Exception ex)
        //    {
        //        ApiResponse oApiResponse = new ApiResponse();
        //        oApiResponse.Data = null;
        //        oApiResponse.Errors = ex.Message;
        //        oApiResponse.CodeStatus = "502";
        //        throw new Exception();
        //    }
        
        //}
    }
}
