using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CinemaApiCase.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class StaffOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if request has 'isStaff' header
            if (!context.HttpContext.Request.Headers.TryGetValue("isStaff", out var isStaffValue))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // ensure that the value of 'isStaff' is "true"
            if (!bool.TryParse(isStaffValue, out bool isStaff) || !isStaff)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}