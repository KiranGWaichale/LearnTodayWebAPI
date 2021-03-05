using LearnTodayWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearnTodayWebAPI.Controllers
{
    public class StudentController : ApiController
    {
        private LearnTodayWebAPIDbContext dbContext;
        public StudentController() 
        {
            dbContext = new LearnTodayWebAPIDbContext();
        }
        [HttpGet]
        public IEnumerable<Course> GetAllCourses()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            return dbContext.Courses.OrderBy(c => c.Start_Date).ToList();
        }

        [HttpPost]
        public HttpResponseMessage PostStudent([FromBody]Student student)
        {
            try
            {
                dbContext.Students.Add(student);
                dbContext.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, student);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteStudentEnrollment(int id)
        {
            try
            {
                var entity = dbContext.Students.Where(s => s.EnrollmentId == id).FirstOrDefault();
                if (entity != null)
                {
                    dbContext.Students.Remove(entity);
                    dbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No enrollement information found");
                }

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
