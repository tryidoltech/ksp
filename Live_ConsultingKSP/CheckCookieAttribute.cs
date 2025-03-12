namespace Live_ConsultingKSP
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class CheckCookieAttribute : ActionFilterAttribute
    {
        private readonly string _cookieName;

        public CheckCookieAttribute(string cookieName)
        {
            _cookieName = cookieName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the cookie exists
            var hasCookie = context.HttpContext.Request.Cookies.ContainsKey(_cookieName);

            if (!hasCookie)
            {
                // Redirect to the login page if the cookie is missing
                context.Result = new RedirectToActionResult("Index", "CompanySetup", null);
            }

            base.OnActionExecuting(context);
        }
    }

}
