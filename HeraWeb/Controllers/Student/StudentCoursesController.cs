using System.Collections.Generic;
using System.Threading.Tasks;
using HeraServices.Services.ApplicationServices;
using HeraServices.Services.UserServices;
using HeraServices.ViewModels.EntitiesViewModels;
using HeraWeb.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeraWeb.Controllers.Student
{
    [Produces("application/json")]
    [Route("api/Student/Courses/[action]")]
    [Authorize(Roles = "Estudiante")]
    public class StudentCoursesController : Controller
    {
        private readonly UserService _userService;
        private readonly EstudianteService _estudianteService;

        public StudentCoursesController(
            UserService userService,
            EstudianteService estudianteService)
        {
            _userService = userService;
            _estudianteService = estudianteService;
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourse(int courseId)
        {
            return await this.Get(async () =>
            {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.Get_Curso(estId, courseId);
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCourses(
            [FromQuery]string searchString = "",
            [FromQuery]int skip = 0,
            [FromQuery]int take = 10)
        {
            return await this.Get(async () =>
            {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.GetAll_Curso(estId, searchString, skip, take);
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUnenrolledCourses(
            [FromQuery]string searchString = "",
            [FromQuery]int skip = 0, 
            [FromQuery]int take = 10)
        {
            return await this.Get(async () =>
            {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.GetAll_Curso(estId, searchString, skip, take, true);
            });
        }

        // GET: api/StudentCourses
        [HttpPost]
        public async Task<IActionResult> EnrollStudent([FromBody]AddEstudianteViewModel model)
        {
            return await this.Get(async () =>
            {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.Do_MatricularEstudiante(estId, model);
            });
        }

    }
}
