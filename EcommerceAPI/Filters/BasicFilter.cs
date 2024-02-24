using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;



namespace EcommerceAPI.Filters
{
    public class BasicFilter: ActionFilterAttribute, IActionFilter
    {
      
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Admin: {context.HttpContext.Request.Headers["admin"]}");
            if (context.HttpContext.Request.Headers["Admin"] != "true")
            {
                foreach(var data in context.HttpContext.Request.Headers)
                {
                    Console.WriteLine($"data: {data}");
                }
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
        }
    }
}
