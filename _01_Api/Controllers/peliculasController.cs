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
    public class peliculasController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: api/peliculas
        public IList<pelicula> Getpelicula()
        {
            IList<pelicula> peliculasTabla = db.pelicula.ToList();
            IList<pelicula> Peliculas = new List<pelicula>();
            foreach (var peliculaTabla in peliculasTabla)
            {
                pelicula Pelicula = new pelicula();
                Pelicula.titulo = peliculaTabla.titulo;
                Pelicula.añolanzamiento = peliculaTabla.añolanzamiento;
                Pelicula.sinopsis = peliculaTabla.sinopsis;
                Pelicula.premios = peliculaTabla.premios;
                Pelicula.duracion = peliculaTabla.duracion;
                Pelicula.clasificacion = peliculaTabla.clasificacion;
                Pelicula.presupuesto = peliculaTabla.presupuesto;
                Pelicula.recaudacion = peliculaTabla.recaudacion;

                Peliculas.Add(Pelicula);
            }
            return Peliculas;
        }


        // GET: api/peliculas/5
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult Getpelicula(int id)
        {
            pelicula peliculaTabla = db.pelicula.Find(id);
            if (peliculaTabla == null)
            {
                return NotFound();
            }
            pelicula Pelicula = new pelicula();
            Pelicula.titulo = peliculaTabla.titulo;
            Pelicula.añolanzamiento = peliculaTabla.añolanzamiento;
            Pelicula.sinopsis = peliculaTabla.sinopsis;
            Pelicula.premios = peliculaTabla.premios;
            Pelicula.duracion = peliculaTabla.duracion;
            Pelicula.clasificacion = peliculaTabla.clasificacion;
            Pelicula.presupuesto = peliculaTabla.presupuesto;
            Pelicula.recaudacion = peliculaTabla.recaudacion;


            return Ok(Pelicula);
        }


        // PUT: api/peliculas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpelicula(int id, pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pelicula.id)
            {
                return BadRequest();
            }

            db.Entry(pelicula).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!peliculaExists(id))
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

        // POST: api/peliculas
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult Postpelicula(pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.pelicula.Add(pelicula);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pelicula.id }, pelicula);
        }

        // DELETE: api/peliculas/5
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult Deletepelicula(int id)
        {
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            db.pelicula.Remove(pelicula);
            db.SaveChanges();

            return Ok(pelicula);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool peliculaExists(int id)
        {
            return db.pelicula.Count(e => e.id == id) > 0;
        }
    }
}