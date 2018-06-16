using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeraWeb.Controllers.Teacher
{
    [Produces("application/json")]
    [Route("api/Teacher")]
    [Authorize(Roles ="Profesor")]
    public class TeacherController : Controller
    {
    }
}