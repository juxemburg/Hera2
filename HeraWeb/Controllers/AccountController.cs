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
using HeraWeb.Services;
using HeraServices.Services.UserServices;

namespace HeraWeb.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly AccountService _accountService;
        private readonly JwtAuthenticationService _tokenService;
        private readonly UserService _userService;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            JwtAuthenticationService tokenService,
            AccountService accountService,
            UserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _accountService = accountService;
            _tokenService = tokenService;
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            return await this.Post(ModelState, async () =>
            {
                var result =  await _accountService.Login(model);
                if(result.Success)
                {
                    result.Value.Token = await _tokenService.BuildToken(model.Email);
                }
                return result;
            });
        }



        [HttpPost]
        public async Task<IActionResult> RegisterTeacher([FromBody]RegisterProfesorViewModel model)
        {
            return await this.Post(ModelState, async () =>
            {
                var result  = await _accountService.RegisterProfesor(model);
                result.Value.Token = await _tokenService.BuildToken(model.Email);
                return result;
            });
        }

        
        [HttpGet]
        public async Task<IActionResult> GetStudentRegistrationMetadata()
        {
            return await this.Get(() =>
            {
                return _accountService.GetEstudianteRegistrationMetadata();
            });
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent([FromBody]RegisterEstudianteViewModel model)
        {
            return await this.Post(ModelState, async () =>
            {
                var result = await _accountService.RegisterEstudiante(model);
                result.Value.Token = await _tokenService.BuildToken(model.Email);
                return result;
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public IActionResult IsAuthenticated()
        {
            return Ok(true);
        }

        [HttpGet("{role}")]
        [Authorize]
        public Task<IActionResult> IsInRole(string role)
        {
            return this.Get(() => {
                return _userService.Get_IsInRole(User.Claims, role);
            });
        }
        
        [HttpGet]
        [Authorize]
        public object Claims()
        {
            return User.Claims.Select(c =>
            new
            {
                Type = c.Type,
                Value = c.Value
            });
        }

    }
}
