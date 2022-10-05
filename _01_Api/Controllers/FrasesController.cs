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
    public class FrasesController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: api/Frases
        public IQueryable<frase> Getfrase()
        {
            return db.frase;
        }

        // GET: api/Frases/5
        [ResponseType(typeof(frase))]
        public IHttpActionResult Getfrase(int id)
        {
            frase frase = db.frase.Find(id);
            if (frase == null)
            {
                return NotFound();
            }

            return Ok(frase);
        }

        // PUT: api/Frases/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfrase(int id, frase frase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != frase.id)
            {
                return BadRequest();
            }

            db.Entry(frase).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!fraseExists(id))
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

        // POST: api/Frases
        [ResponseType(typeof(frase))]
        public IHttpActionResult Postfrase(frase frase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.frase.Add(frase);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = frase.id }, frase);
        }

        // DELETE: api/Frases/5
        [ResponseType(typeof(frase))]
        public IHttpActionResult Deletefrase(int id)
        {
            frase frase = db.frase.Find(id);
            if (frase == null)
            {
                return NotFound();
            }

            db.frase.Remove(frase);
            db.SaveChanges();

            return Ok(frase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool fraseExists(int id)
        {
            return db.frase.Count(e => e.id == id) > 0;
        }
    }
}