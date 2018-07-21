using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeraWeb.Controllers.Student
{
    [Produces("application/json")]
    [Route("api/Student/Courses/[action]")]
    [Authorize(Roles = "Estudiante")]
    public class StudentCoursesController : Controller
    {
        public StudentCoursesController()
        {

        }

        // GET: api/StudentCourses
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/StudentCourses/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/StudentCourses
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/StudentCourses/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
