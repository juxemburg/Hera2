using Entities.Usuarios;
using HeraDAL.DataAcess;
using HeraServices.Services.UserServices;
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
        private readonly UserService _userService;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            UserService userService,
            IDataAccess dataAccess)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _dataAccess = dataAccess;
            _userService = userService;
        }

        public async Task<ApiResult<UserInfoViewModel>> Login(LoginViewModel model, string returnUrl = "")
        {
            var apiResult = ApiResult<UserInfoViewModel>.Initialize(null);
            var result = await _signInManager
                    .PasswordSignInAsync(model.Email, model.Password,
                    model.RememberMe, lockoutOnFailure: false);

            apiResult.Success = result.Succeeded;
            if (!result.Succeeded)
            {
                apiResult.AddError("", "Correo y/o contraseña inválidos.");
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                apiResult.Value = await _userService.Get_UserInfo(user.UsuarioId);
            }


            return apiResult;
        }

        public async Task<ApiResult<UserInfoViewModel>> RegisterProfesor
            (RegisterProfesorViewModel model)
        {

            return await RegisterUser(model, "Profesor",
                    (usuarioId) =>
                    {
                        _dataAccess.AddProfesor(model.Map(usuarioId));
                    });
        }

        public async Task<ApiResult<UserInfoViewModel>> RegisterEstudiante(RegisterEstudianteViewModel model)
        {
            return await RegisterUser(model, "Estudiante",
                    (usuarioId) =>
                    {
                        _dataAccess.AddEstudiante(model.Map(usuarioId));
                    });
        }

        private async Task<ApiResult<UserInfoViewModel>> RegisterUser(RegisterViewModel model,
            string role, Action<int> userCreation)
        {
            var apiResult = ApiResult<UserInfoViewModel>.Initialize(null);

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
                    apiResult.Success = true;
                    apiResult.Value = await _userService.Get_UserInfo(user.UsuarioId);
                    return apiResult;
                }
                else
                {
                    apiResult.AddError("", result.Errors.ToString());
                    return apiResult;
                }

            }
            catch (Exception e)
            {
                return apiResult;
            }

        }
    }
}
