using System.Web;
using System.Web.Mvc;

namespace SourceControlAssignment.Attributes
{
    /// <summary>
    /// <c>BasicAuthAttribute</c> custom authentication attribute to check user is logged in or not using session variable UserID.
    /// </summary>
    public class BasicAuthAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// <c>AuthorizeCore</c> authorizes the user.
        /// </summary>
        /// <param name="actionContext">Action context of http request.</param>
        /// <returns>True if user is logged in, False if user is not logged in.</returns>
        protected override bool AuthorizeCore(HttpContextBase actionContext)
        {
            bool authorize = false;
            if (HttpContext.Current.Session["UserID"] != null)
            {
                authorize = true;
            }
            return authorize;
        }

        /// <summary>
        /// <c>HandleUnauthorizedRequest</c> Handles unauthorized requests.
        /// <description>Redirects the unauthorized requests to login page.</description>
        /// </summary>
        /// <param name="filterContext">Authorization context of http request.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string url = filterContext.HttpContext.Request.Url.ToString();
            url = HttpUtility.UrlDecode(url);
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
            {
                { "Controller", "Account"},
                { "Action", "Login"},
                { "returnUrl", url}
            });
        }
    }
}