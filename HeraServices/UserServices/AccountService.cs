using Entities.Usuarios;
using HeraDAL.DataAcess;
using HeraServices.ViewModels.AccountViewModels;
using HeraServices.ViewModels.ApiViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HeraServices.UserServices
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDataAccess _dataAccess;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IDataAccess dataAccess)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dataAccess = dataAccess;
        }

        public async Task<ApiResult<bool>> Login(LoginViewModel model, string returnUrl= "")
        {
            var apiResult = new ApiResult<bool>()
            {
                ModelErrors = new Dictionary<string, string>(),
                Value = false
            };
            var result = await _signInManager
                    .PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                apiResult.ModelErrors.Add("", "Correo y/o contraseña inválidos.");
            }
            apiResult.Value = result.Succeeded;

            return apiResult;
        }

        public async Task<ApiResult<bool>> RegisterProfesor
            (RegisterProfesorViewModel model, string returnUrl = null)
        {
            var apiResult = new ApiResult<bool>()
            {
                ModelErrors = new Dictionary<string, string>(),
                Value = false
            };
            var res = await RegisterUser(model, "Profesor",
                    (usuarioId) =>
                    {
                        _dataAccess.AddProfesor(model.Map(usuarioId));
                    });

            apiResult.Value = res;
            return apiResult;

        }

        private async Task<bool> RegisterUser(RegisterViewModel model,
            string role, Action<int> userCreation)
        {

            try
            {
                var user
                    = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };
                var result
                    = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user,
                    new Claim("UsuarioId", "" + user.UsuarioId));
                    var roleResult =
                        await _userManager.AddToRoleAsync(user, role);

                    if (roleResult.Succeeded)
                    {
                        userCreation(user.UsuarioId);
                    }
                    await _dataAccess.SaveAllAsync();
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account",
                    //    new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirma tu cuenta",
                    //    $"Por favor confirma tu correo al hacer click aquí: <a href='{callbackUrl}'>link</a>");
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
