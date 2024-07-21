using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LapShopProject.Filters
{
    public class ConvertJsonToIntAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Assuming the parameter name is "intValue" in the request data
            if (context.ActionArguments.TryGetValue("intValue", out var value))
            {
                if (value is string stringValue && int.TryParse(stringValue, out var intValue))
                {
                    // Successfully converted to int
                    context.ActionArguments["intValue"] = intValue;
                }
                else
                {
                    // Handle invalid input (e.g., return an error response)
                    context.Result = new BadRequestObjectResult("Invalid integer value.");
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
