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
    public class CompanyiasController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: api/Companyias
        public IQueryable<companyia> Getcompanyia()
        {
            return db.companyia;
        }

        // GET: api/Companyias/5
        [ResponseType(typeof(companyia))]
        public IHttpActionResult Getcompanyia(int id)
        {
            companyia companyia = db.companyia.Find(id);
            if (companyia == null)
            {
                return NotFound();
            }

            return Ok(companyia);
        }

        // PUT: api/Companyias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcompanyia(int id, companyia companyia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyia.id)
            {
                return BadRequest();
            }

            db.Entry(companyia).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!companyiaExists(id))
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

        // POST: api/Companyias
        [ResponseType(typeof(companyia))]
        public IHttpActionResult Postcompanyia(companyia companyia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.companyia.Add(companyia);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = companyia.id }, companyia);
        }

        // DELETE: api/Companyias/5
        [ResponseType(typeof(companyia))]
        public IHttpActionResult Deletecompanyia(int id)
        {
            companyia companyia = db.companyia.Find(id);
            if (companyia == null)
            {
                return NotFound();
            }

            db.companyia.Remove(companyia);
            db.SaveChanges();

            return Ok(companyia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool companyiaExists(int id)
        {
            return db.companyia.Count(e => e.id == id) > 0;
        }
    }
}