using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Buffers;
using Microsoft.AspNetCore.Mvc.Formatters;



namespace LapShopProject.Filters
{

    /// <summary>
    /// This filter ignore refernce loop after the action executed and before serialize object to json file
    /// </summary>

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class IgnoreReferenceLoopAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Formatters.Add(new NewtonsoftJsonOutputFormatter(
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    },
                    context.HttpContext.RequestServices.GetRequiredService<ArrayPool<char>>(),
                    context.HttpContext.RequestServices.GetRequiredService<IOptions<MvcOptions>>().Value
                ));
            }
        }
    }
}
