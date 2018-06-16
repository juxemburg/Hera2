using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Entities.Usuarios;
using HeraServices.ViewModels.AccountViewModels;
using HeraWeb.Utils;
using HeraServices.UserServices;
using Microsoft.AspNetCore.Authorization;

namespace HeraWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly AccountService _accountService;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            AccountService accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {

            return await this.InsertModel<LoginViewModel>(model, ModelState, async () => {
                return await _accountService.Login(model);
            });

        }



        [HttpPost]
        public async Task<IActionResult> RegisterTeacher([FromBody]RegisterProfesorViewModel model)
        {
            return await this.InsertModel<RegisterProfesorViewModel>(model, ModelState, async () => {
                return await this._accountService.RegisterProfesor(model);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult IsAuthenticated()
        {
            
            return Ok();
        }

    }
}
