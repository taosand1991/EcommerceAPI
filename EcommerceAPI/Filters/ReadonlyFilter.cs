using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceAPI.Filters
{
    public class ReadonlyFilter: ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers["Readonly"] != "true")
            {
                foreach (var data in context.HttpContext.Request.Headers)
                {
                    Console.WriteLine($"data: {data}");
                }
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
