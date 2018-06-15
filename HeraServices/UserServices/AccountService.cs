using Entities.Usuarios;
using HeraDAL.DataAcess;
using HeraServices.ViewModels.AccountViewModels;
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

        public async Task<bool> RegisterProfesor
            (RegisterProfesorViewModel model, string returnUrl = null)
        {
            return await RegisterUser(model, "Profesor",
                    (usuarioId) =>
                    {
                        _dataAccess.AddProfesor(model.Map(usuarioId));
                    });

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
