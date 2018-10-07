using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeraServices.ApplicationServices;
using HeraServices.Services.UserServices;
using HeraWeb.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeraWeb.Controllers.Teacher
{
    [Produces("application/json")]
    [Authorize(Roles = "Profesor")]
    [Route("api/Teacher/Course/{courseId}/Student/{studentId}/[action]")]
    public class TeacherCourseStudentController : Controller
    {
        private readonly ProfesorCursoService _mgrService;
        private readonly UserService _userService;

        public TeacherCourseStudentController(
            ProfesorCursoService mgrService, 
            UserService userService)
        {
            _mgrService = mgrService;
            _userService = userService;
        }

        // GET: api/TeacherCourseStudent
        [HttpGet]
        public async Task<IActionResult> Traces(int courseId, int studentId) =>
            await this.Get(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _mgrService.Get_StudentTraces(teacherId, courseId, studentId);
            });

    }
}
