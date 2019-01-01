using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeraServices.ApplicationServices;
using HeraServices.Services.ApplicationServices;
using HeraServices.Services.UserServices;
using HeraServices.ViewModels.EntitiesViewModels.ProfesorEstudiante;
using HeraWeb.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeraWeb.Controllers.Teacher
{
    [Produces("application/json")]
    [Route("api/Teacher/Courses")]
    [Authorize(Roles = "Profesor")]
    public class TeacherCoursesController : Controller
    {
        private readonly UserService _userService;
        private readonly CursoService _cursoService;
        private readonly ProfesorService _ctrlService;
        private readonly ProfesorCursoService _profesorCursoService;

        public TeacherCoursesController(
            UserService userService,
            CursoService cursoService,
            ProfesorService ctrlService,
            ProfesorCursoService profesorCursoService)
        {
            _cursoService = cursoService;
            _userService = userService;
            _ctrlService = ctrlService;
            _profesorCursoService = profesorCursoService;
        }

        // GET: api/TeacherCourses
        [HttpGet("")]
        public async Task<IActionResult> GetCourses([FromQuery]string searchString = "",
            [FromQuery]int skip = 0, [FromQuery]int take = 10) =>
            await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.GetAll_Cursos(teacherId, searchString, skip, take);
            });


        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourse(int courseId) =>
            await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _profesorCursoService.Get_Curso(teacherId, courseId);
            });

        [HttpGet("{courseId}/Student/{studentId}/Grades")]
        public async Task<IActionResult> GetStudentGrades(int courseId, int studentId) =>
            await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _profesorCursoService.Get_RegistrosEstudiante(teacherId, courseId, studentId);
            });

        [HttpPost("{courseId}/Challenge/{challengeId}")]
        public async Task<IActionResult> AddChallenge(int courseId, int challengeId) =>
            await this.Post(ModelState, async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _cursoService.Add_DesafioCurso(teacherId, courseId, challengeId);
            });

        [HttpPost("{courseId}/SortChallenges")]
        public async Task<IActionResult> SortChallenges(int courseId, [FromBody]List<int> model) =>
            await this.Post(ModelState, async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _profesorCursoService.SortChallenges(teacherId, courseId, model);
            });

        [HttpPost("{courseId}/Student/{studentId}/Challenge/{challengeId}/Valoration")]
        public async Task<IActionResult> PostStudentValoration(
            int courseId,
            int studentId,
            int challengeId,
            [FromBody] CreateCalificacionCualitativaViewModel model) =>
            await this.Post(ModelState,
            async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _profesorCursoService.Do_Calificar(teacherId, courseId, studentId, challengeId, model);
            });

    }
}
