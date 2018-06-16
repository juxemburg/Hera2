using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Cursos;
using HeraServices.ViewModels.ApiViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HeraWeb.Utils;
using HeraServices.Services.ApplicationServices;
using HeraServices.Services.UserServices;
using HeraServices.ViewModels.UtilityViewModels;

namespace HeraWeb.Controllers.Teacher
{
    [Produces("application/json")]
    [Route("api/Teacher")]
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

        [HttpGet("Courses")]
        public async Task<IActionResult> GetCourses([FromQuery]string searchString = "",
            [FromQuery]int skip = 0, [FromQuery]int take = 10)
        {
            return await this.GetModel(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.GetAll_Cursos(teacherId, searchString, skip, take);
            });
        }

        [HttpGet("DisabledCourses")]
        public async Task<IActionResult> GetDisabledCourses([FromQuery]string searchString = "",
            [FromQuery]int skip = 0, [FromQuery]int take = 10)
        {
            return await this.GetModel(async () =>
            {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.GetAll_CursosI(teacherId, searchString, skip, take);
            });
        }

    }
}