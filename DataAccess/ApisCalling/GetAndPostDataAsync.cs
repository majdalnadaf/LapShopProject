using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models;
using Newtonsoft.Json;

namespace DataAccess.ApisCalling
{

    public class ApiResponse
    {
        public object Data { get; set; }
        public object Errors { get; set; }
        public string CodeStatus { get; set; }
    }
    public class GetAndPostDataAsync<T>
    {
        static HttpClient Client = new HttpClient();
        public async Task<ApiResponse> GetAsync(string pathApi) 
        {
            ApiResponse oApiResponse = null;

            HttpResponseMessage responeMessage =await Client.GetAsync(pathApi);
            if (responeMessage.IsSuccessStatusCode) 
            {
                oApiResponse = await responeMessage.Content.ReadAsAsync<ApiResponse>();
            }


            return oApiResponse;
        
        }


        public async Task<Uri> PostAsynce(T model , string pathApi)  // you must build base class and use it as data type for paramter in this method or use T in Generic class 
        {

            HttpResponseMessage responseMessage =  await Client.PostAsJsonAsync(pathApi, model);
            responseMessage.EnsureSuccessStatusCode();

            return responseMessage.Headers.Location;
        }

    }
}
