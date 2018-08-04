using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeraServices.Services.ApplicationServices;
using HeraServices.Services.UserServices;
using HeraWeb.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeraWeb.Controllers.Student
{

    [Produces("application/json")]
    [Authorize(Roles = "Estudiante")]
    [Route("api/Student/Course/{courseId}/Challenge")]
    public class StudentChallengeController : Controller
    {
        private readonly UserService _userService;
        private readonly EstudianteService _estudianteService;

        public StudentChallengeController(
            UserService userService,
            EstudianteService estudianteService)
        {
            _userService = userService;
            _estudianteService = estudianteService;
        }

        [HttpGet("{challengeId}")]
        public async Task<IActionResult> GetChallenge(int courseId, int challengeId)
        {
            return await this.Get(async () => {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.Get_Desafio(estId, courseId, challengeId);
            });
        }


    }
}
