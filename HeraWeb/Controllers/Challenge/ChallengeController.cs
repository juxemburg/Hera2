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
using Microsoft.AspNetCore.Authorization;
using HeraServices.ViewModels.UtilityViewModels;
using HeraServices.ViewModels.EntitiesViewModels.Desafios;
using HeraServices.Services;
using HeraServices.ViewModels.ApiViewModels;
using HeraServices.ViewModels.ServicesViewModels.Valoration;

namespace HeraWeb.Controllers.Challenge
{
    [Authorize(Roles = "Profesor")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ChallengeController : Controller
    {
        private readonly UserService _userService;
        private readonly DesafioService _ctrlService;

        public ChallengeController(UserService userService, DesafioService ctrlService)
        {
            _userService = userService;
            _ctrlService = ctrlService;
        }

        public async Task<IActionResult> GetGeneralValoration([FromQuery]string proyectId)
        {
            return await this.Get(async () => {
                return await _ctrlService.GetValoration(proyectId);
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddChallenge([FromBody] CreateDesafioViewModel model)
        {
            return await this.Post(model, ModelState, async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.Create_Desafio(teacherId, model);
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetChallenges([FromBody]SearchDesafioViewModel model,
            [FromQuery]int skip = 0, [FromQuery]int take = 10)
        {
            return await this.Post(model, ModelState, async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.GetAll_Desafios(teacherId, model, skip, take);
            });
        }

        

    }
}