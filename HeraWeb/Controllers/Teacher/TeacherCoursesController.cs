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
        private readonly ProfesorService _ctrlService;
        private readonly ProfesorCursoService _profesorCursoService;

        public TeacherCoursesController(
            UserService userService,
            ProfesorService ctrlService,
            ProfesorCursoService profesorCursoService)
        {
            _userService = userService;
            _ctrlService = ctrlService;
            _profesorCursoService = profesorCursoService;
        }

        // GET: api/TeacherCourses
        [HttpGet("")]
        public async Task<IActionResult> GetCourses([FromQuery]string searchString = "",
            [FromQuery]int skip = 0, [FromQuery]int take = 10)
        {
            return await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.GetAll_Cursos(teacherId, searchString, skip, take);
            });
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourse(int courseId)
        {
            return await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _profesorCursoService.Get_Curso(teacherId, courseId);
            });
        }

        [HttpGet("{courseId}/Student/{studentId}/Grades")]
        public async Task<IActionResult> GetStudentGrades(int courseId, int studentId)
        {
            return await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _profesorCursoService.Get_RegistrosEstudiante(teacherId, courseId, studentId);
            });
        }


        [HttpPost("{courseId}/Student/{studentId}/Challenge/{challengeId}/Valoration")]
        public async Task<IActionResult> PostStudentValoration(
            int courseId,
            int studentId,
            int challengeId,
            [FromBody] CreateCalificacionCualitativaViewModel model)
        {
            return await this.Post(model, ModelState,
                async () =>
                {
                    var teacherId = _userService.Get_ProfesorId(User.Claims);
                    return await _profesorCursoService.Do_Calificar(teacherId, courseId, studentId, challengeId, model);
                });
        }

    }
}
