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
    public class ActoresController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: api/Actores
        public IQueryable<actor> Getactor()
        {
            return db.actor;
        }

        // GET: api/Actores/5
        [ResponseType(typeof(actor))]
        public IHttpActionResult Getactor(int id)
        {
            actor actor = db.actor.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

        // PUT: api/Actores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putactor(int id, actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actor.id)
            {
                return BadRequest();
            }

            db.Entry(actor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!actorExists(id))
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

        // POST: api/Actores
        [ResponseType(typeof(actor))]
        public IHttpActionResult Postactor(actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.actor.Add(actor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = actor.id }, actor);
        }

        // DELETE: api/Actores/5
        [ResponseType(typeof(actor))]
        public IHttpActionResult Deleteactor(int id)
        {
            actor actor = db.actor.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            db.actor.Remove(actor);
            db.SaveChanges();

            return Ok(actor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool actorExists(int id)
        {
            return db.actor.Count(e => e.id == id) > 0;
        }
    }
}