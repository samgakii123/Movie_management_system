using DeltaX.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeltaX.Controllers
{
    public class MovieController : ApiController
    {
        DeltaXModel db = new DeltaXModel();
        [ActionName("get"), HttpGet]
        public IEnumerable<MovieList> GetAllMovie()
        {
            var movieList = db.Database.SqlQuery<MovieList>("exec uspSelectAllMovie @MovieId",new SqlParameter("@MovieId",DBNull.Value)).ToList();
            return movieList;
        }
        [ActionName("post"), HttpPost]
        public HttpResponseMessage AddMovie([FromBody]Movy movie )
        {
            movie.CreatedBy = 1;
            string actorIds = string.Empty;

            foreach (string id in movie.ActorId)
                actorIds += id + "|";
            
                var res = db.Database.SqlQuery<Movy>("exec uspAddUpdateMovie @MovieId,@ProducerId,@MovieName," +
                "@ActorId, @ReleaseYear, @Poster, @Plot, @CreatedBy",
                new SqlParameter("@MovieId", movie.MovieId),
                new SqlParameter("@ProducerId", movie.ProducerId),
                new SqlParameter("@MovieName", movie.MovieName),
                new SqlParameter("@ActorId", actorIds.TrimEnd('|')),
                new SqlParameter("@ReleaseYear", movie.ReleaseYear),
                new SqlParameter("@Poster", movie.Poster),
                new SqlParameter("@Plot", movie.Plot),
                new SqlParameter("@CreatedBy", 1)).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.Created, res);
            
            
        }

        public MovieList GetMovieById(int id)
        {
            var movieList = db.Database.SqlQuery<MovieList>("exec uspSelectAllMovie @MovieId", new SqlParameter("@MovieId", id)).ToList();
            return movieList.FirstOrDefault();
        }
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            Movy movie = db.Movies.Where(m => m.MovieId == id).FirstOrDefault();
            movie.UpdatedBy = 1;
            movie.UpdatedDt = DateTime.Now;
            if (movie.Active)
                movie.Active = false;
            else
                movie.Active = true;
            db.Entry(movie).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Record removed Successfully");
        }
    }
}
