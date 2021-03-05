using LearnTodayWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearnTodayWebAPI.Controllers
{
    public class AdminController : ApiController
    {
        private LearnTodayWebAPIDbContext dbContext;
        public AdminController()
        {
           
            dbContext = new LearnTodayWebAPIDbContext();
            dbContext.Configuration.ProxyCreationEnabled = false;
        }

        [HttpGet]
        public IEnumerable<Course> GetAllCourses()
        {
            return dbContext.Courses.ToList();
        }

        [HttpGet]
        public HttpResponseMessage GetCourseById(int id)
        {
            var entity = dbContext.Courses.Where(c => c.CourseId == id).FirstOrDefault();

            if (entity != null)
            {
                return Request.CreateResponse<Course>(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Searched Data not Found.");
            }
            
        }
    }
}