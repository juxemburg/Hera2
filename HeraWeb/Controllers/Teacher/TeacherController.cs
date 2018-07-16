using System.Threading.Tasks;
using HeraServices.Services.ApplicationServices;
using HeraServices.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HeraWeb.Utils;
using Microsoft.AspNetCore.Authorization;

namespace HeraWeb.Controllers.Teacher
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Profesor")]
    public class TeacherController : Controller
    {
        private readonly UserService _userService;
        private readonly ProfesorService _ctrlService;

        public TeacherController(UserService userService, ProfesorService ctrlService)
        {
            _userService = userService;
            _ctrlService = ctrlService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses([FromQuery]string searchString = "",
            [FromQuery]int skip = 0, [FromQuery]int take = 10)
        {
            return await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.GetAll_Cursos(teacherId, searchString, skip, take);
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursesList([FromQuery]string searchString = "")
        {
            return await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.GetAll_CursosList(teacherId, searchString);
            });
        }

        [HttpGet("DisabledCourses")]
        public async Task<IActionResult> GetDisabledCourses([FromQuery]string searchString = "",
            [FromQuery]int skip = 0, [FromQuery]int take = 10)
        {
            return await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.GetAll_CursosI(teacherId, searchString, skip, take);
            });
        }
    }
}
