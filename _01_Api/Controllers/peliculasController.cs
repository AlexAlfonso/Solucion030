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
        public IHttpActionResult GetEmpleado(int? id, int? siguiente)
        {

            pelicula peliculaTabla = null;
            if (siguiente == null)
            {
                peliculaTabla = db.pelicula.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == 1)
                {
                    peliculaTabla = db.pelicula.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<pelicula> PeliculasTablas = db.pelicula.Where(x => x.id < id.Value).ToList();
                    if (PeliculasTablas != null && PeliculasTablas.Count() > 0)
                    {
                        int? idpelicula = PeliculasTablas.Max(x => x.id);
                        peliculaTabla = db.pelicula.Where(x => x.id == idpelicula.Value).FirstOrDefault();
                    }
                }
            }
            if (peliculaTabla == null)
            {
                peliculaTabla = db.pelicula.Where(x => x.id == id.Value).FirstOrDefault();
            }
            pelicula miPelicula = new pelicula();
            miPelicula.titulo = peliculaTabla.titulo;
            miPelicula.añolanzamiento = peliculaTabla.añolanzamiento;
            miPelicula.sinopsis = peliculaTabla.sinopsis;
            miPelicula.premios = peliculaTabla.premios;
            miPelicula.duracion = peliculaTabla.duracion;
            miPelicula.clasificacion = peliculaTabla.clasificacion;
            miPelicula.presupuesto = peliculaTabla.presupuesto;
            miPelicula.recaudacion = peliculaTabla.recaudacion;

            pelicula otraPelicula = miPelicula;

            return Ok(miPelicula);


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