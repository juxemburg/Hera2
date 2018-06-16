using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeraServices.Services.ApplicationServices;
using HeraServices.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HeraWeb.Utils;
using HeraServices.ViewModels.EntitiesViewModels;

namespace HeraWeb.Controllers.Challenge
{
    [Produces("application/json")]
    [Route("api/Challenge")]
    public class ChallengeController : Controller
    {
        private readonly UserService _userService;
        private readonly DesafioService _ctrlService;

        public ChallengeController(UserService userService, DesafioService ctrlService)
        {
            _userService = userService;
            _ctrlService = ctrlService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddChallenge([FromBody] CreateDesafioViewModel model)
        {
            return await this.InsertModel(model, ModelState, async () => {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.Create_Desafio(teacherId, model);
            });
        }

    }
}