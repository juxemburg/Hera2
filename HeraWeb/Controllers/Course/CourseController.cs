using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeraServices.Services.ApplicationServices;
using HeraServices.Services.UserServices;
using HeraServices.ViewModels.EntitiesViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HeraWeb.Utils;

namespace HeraWeb.Controllers.Course
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Profesor")]
    public class CourseController : Controller
    {
        private readonly UserService _userService;
        private CursoService _ctrlService;

        public CourseController(UserService userService, CursoService ctrlService)
        {
            _userService = userService;
            _ctrlService = ctrlService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody]CreateCursoViewModel model)
        {
            return await this.Post(model, ModelState, async () => {
                var teacherId = _userService.Get_ProfesorId(User.Claims);
                return await _ctrlService.Create_Curso(teacherId, model);
            });
        }
    }
}