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

        // GET: api/StudentCourses
        [HttpPost]
        public async Task<IActionResult> EnrollStudent([FromBody]AddEstudianteViewModel model)
        {
            return await this.Get(async () => {
                var estId = _userService.Get_EstudianteId(User.Claims);
                return await _estudianteService.Do_MatricularEstudiante(estId, model);
            });
        }
        
    }
}
