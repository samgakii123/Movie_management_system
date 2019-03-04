using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeltaX.Models;
namespace DeltaX.Controllers
{
    public class ActorApiController : ApiController
    {
        DeltaXModel db = new DeltaXModel();
        [ActionName("get"), HttpGet]
        public IEnumerable<Actor> GetAllProducer()
        {
            return db.Actors.OrderBy(A => A.Name).ToList();
        }
        [ActionName("post"), HttpPost]
        public HttpResponseMessage AddActor([FromBody]Actor actor)
        {
            actor.CreatedBy = 1;
            actor.CreatedDt = DateTime.Now;
            if (actor.ActorId > 0)
            {
                actor.UpdatedBy = 1;
                actor.UpdatedDt = DateTime.Now;
                db.Entry(actor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, actor);
            }
            else if (db.Actors.Where(m => m.Name.Equals(actor.Name)).FirstOrDefault() != null)
            {
                return Request.CreateResponse(HttpStatusCode.Found, db.Actors.Where(m => m.Name.Equals(actor.Name)).FirstOrDefault());
            }
            db.Actors.Add(actor);
            db.SaveChanges();
            var respons = Request.CreateResponse(HttpStatusCode.Created, actor);
            return respons;
        }
       
        public Actor GetActorById(int id)
        {
            return db.Actors.Where(m => m.ActorId == id).FirstOrDefault();
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Actor actor=db.Actors.Where(m => m.ActorId == id).FirstOrDefault();
            actor.UpdatedBy = 1;
            actor.UpdatedDt = DateTime.Now;
            if (actor.Active)
                actor.Active = false;
            else
                actor.Active = true;
            db.Entry(actor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Record removed Successfully");
        }
    }
}
