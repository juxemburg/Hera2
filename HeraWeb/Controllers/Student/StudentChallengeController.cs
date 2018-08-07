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
    [Route("api/Student/Course/{courseId}/Challenge/{challengeId}/[action]")]
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

        [HttpGet]
        public async Task<IActionResult> Get(int courseId, int challengeId)
        {
            return await this.Get(async () =>
            {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.Get_Desafio(estId, courseId, challengeId);
            });
        }

        [HttpGet("{noteId}")]
        public async Task<IActionResult> Start(int courseId, int challengeId, int noteId)
        {
            return await this.Get(async () =>
            {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.Do_IniciarDesafio(estId, courseId, challengeId, noteId);
            });
        }

        [HttpGet("{noteId}")]
        public async Task<IActionResult> End(int courseId, int challengeId, int noteId, [FromQuery]string projId)
        {
            return await this.Get(async () =>
            {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.Do_CalificarDesafio(estId, courseId, challengeId, noteId, projId);
            });
        }

        [HttpGet]
        public async Task<IActionResult> CreateRecord(int courseId, int challengeId)
        {
            return await this.Get(async () =>
            {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.Do_AddCalificacion(estId, courseId, challengeId);
            });
        }
    }
}
