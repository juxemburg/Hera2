using Entities.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraWeb.Filters
{
    public class ApiAuthorize : AuthorizeAttribute
    {
        private SignInManager<ApplicationUser> _signInManager;

        public ApiAuthorize(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            if(_signInManager.IsSignedIn(context.HttpContext.User))
            {
                await next();
            }
            
        }
    }
}
