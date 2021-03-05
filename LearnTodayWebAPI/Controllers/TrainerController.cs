using LearnTodayWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearnTodayWebAPI.Controllers
{
    public class TrainerController : ApiController
    {
        private LearnTodayWebAPIDbContext dbContext;
        public TrainerController()
        {
            dbContext = new LearnTodayWebAPIDbContext();
        }

        [HttpPost]
        public HttpResponseMessage TrainerSignUp([FromBody]Trainer trainer)
        {
            try 
            {
                dbContext.Trainers.Add(trainer);
                dbContext.SaveChanges();

                return Request.CreateResponse<Trainer>(HttpStatusCode.Created, trainer);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdatePassword([FromUri]int id, [FromBody]Trainer trainer)
        {
            try
            {
                var entity = dbContext.Trainers.Where(t => t.TrainerId == id).FirstOrDefault();
                if (entity != null)
                {
                    entity.Password = trainer.Password;
                    dbContext.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "Data updated successfully");
                }
                else
                { 
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Searched Data not Found.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
