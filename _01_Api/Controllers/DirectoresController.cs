using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _04_Data.Datos;

namespace _01_Api.Controllers
{
    public class DirectoresController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: api/Directores
        public IQueryable<director> Getdirector()
        {
            return db.director;
        }

        // GET: api/Directores/5
        [ResponseType(typeof(director))]
        public IHttpActionResult Getdirector(int id)
        {
            director director = db.director.Find(id);
            if (director == null)
            {
                return NotFound();
            }

            return Ok(director);
        }

        // PUT: api/Directores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdirector(int id, director director)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != director.id)
            {
                return BadRequest();
            }

            db.Entry(director).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!directorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Directores
        [ResponseType(typeof(director))]
        public IHttpActionResult Postdirector(director director)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.director.Add(director);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = director.id }, director);
        }

        // DELETE: api/Directores/5
        [ResponseType(typeof(director))]
        public IHttpActionResult Deletedirector(int id)
        {
            director director = db.director.Find(id);
            if (director == null)
            {
                return NotFound();
            }

            db.director.Remove(director);
            db.SaveChanges();

            return Ok(director);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool directorExists(int id)
        {
            return db.director.Count(e => e.id == id) > 0;
        }
    }
}