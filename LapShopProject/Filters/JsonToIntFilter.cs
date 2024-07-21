using Microsoft.AspNetCore.Mvc.Filters;

namespace LapShopProject.Filters
{
    public class JsonToIntFilter : IActionFilter
    {


    public void OnActionExecuting(ActionExecutingContext context)
    {
           
            var requestBody = context.HttpContext.Request.Body;
            using var reader = new StreamReader(requestBody);
            var json = reader.ReadToEnd();

           
            if (int.TryParse(json, out var intValue))
            {
                context.ActionArguments["intValue"] = intValue;
            }
    }



        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
