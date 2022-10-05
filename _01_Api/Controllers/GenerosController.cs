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
    public class GenerosController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: api/Generos
        public IQueryable<genero> Getgenero()
        {
            return db.genero;
        }

        // GET: api/Generos/5
        [ResponseType(typeof(genero))]
        public IHttpActionResult Getgenero(int id)
        {
            genero genero = db.genero.Find(id);
            if (genero == null)
            {
                return NotFound();
            }

            return Ok(genero);
        }

        // PUT: api/Generos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putgenero(int id, genero genero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genero.id)
            {
                return BadRequest();
            }

            db.Entry(genero).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!generoExists(id))
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

        // POST: api/Generos
        [ResponseType(typeof(genero))]
        public IHttpActionResult Postgenero(genero genero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.genero.Add(genero);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = genero.id }, genero);
        }

        // DELETE: api/Generos/5
        [ResponseType(typeof(genero))]
        public IHttpActionResult Deletegenero(int id)
        {
            genero genero = db.genero.Find(id);
            if (genero == null)
            {
                return NotFound();
            }

            db.genero.Remove(genero);
            db.SaveChanges();

            return Ok(genero);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool generoExists(int id)
        {
            return db.genero.Count(e => e.id == id) > 0;
        }
    }
}