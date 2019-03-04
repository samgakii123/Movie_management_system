using DeltaX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeltaX.Controllers
{
    public class ProducerApiController : ApiController
    {
        DeltaXModel db = new DeltaXModel();
        [ActionName("get"),HttpGet]
        public IEnumerable<Producer> GetAllProducer()
        {
            return db.Producers.OrderBy(p => p.Name).ToList();
        }
        [ActionName("post"), HttpPost]
        public HttpResponseMessage AddProducer([FromBody]Producer producer)
        {
            producer.CreatedBy = 1;
            producer.CreatedDt = DateTime.Now;
            if (producer.ProducerId > 0)
            {
                producer.UpdatedBy = 1;
                producer.UpdatedDt = DateTime.Now;
                db.Entry(producer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, producer);
            }
            else if (db.Producers.Where(m => m.Name.Equals(producer.Name)).FirstOrDefault() != null)
            {
                return Request.CreateResponse(HttpStatusCode.Found, db.Producers.Where(m => m.Name.Equals(producer.Name)).FirstOrDefault());
            }
            db.Producers.Add(producer);
            db.SaveChanges();
            var respons = Request.CreateResponse(HttpStatusCode.Created, producer);
            return respons;
        }
        public Producer GetProducerById(int id)
        {
            return db.Producers.Where(m => m.ProducerId == id).FirstOrDefault();
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Producer producer = db.Producers.Where(m => m.ProducerId == id).FirstOrDefault();
            producer.UpdatedBy = 1;
            producer.UpdatedDt = DateTime.Now;
            if (producer.Active)
                producer.Active = false;
            else
                producer.Active = true;
            db.Entry(producer).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Record removed Successfully");
        }
    }
}
