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

namespace HeraWeb.Controllers.Teacher
{
    [Produces("application/json")]
    [Route("api/Teacher/Courses")]
    [Authorize(Roles = "Profesor")]
    public class TeacherCoursesController : Controller
    {
        private readonly UserService _userService;
        private readonly ProfesorService _ctrlService;

        public TeacherCoursesController(UserService userService, ProfesorService ctrlService)
        {
            _userService = userService;
            _ctrlService = ctrlService;
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
        
    }
}
