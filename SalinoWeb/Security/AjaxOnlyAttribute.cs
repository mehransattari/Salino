using Microsoft.AspNetCore.Mvc.Filters;

namespace Salino.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers.XRequestedWith == "XMLHttpRequest")
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                throw new InvalidOperationException("This operation can only be accessed via Ajax requests");
            }
        }
    }
}
