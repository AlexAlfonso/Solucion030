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
    public class PeliculasController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        

        // GET: api/Peliculas/
        [ResponseType(typeof(pelicula))]
        public IList<pelicula> Getpelicula()
        {
            IList<pelicula> peliculasTabla = db.pelicula.ToList();
            IList<pelicula> peliculas = new List<pelicula>();
            foreach (var peliculaTabla in peliculasTabla)
            {
                pelicula pelicula = new pelicula();
                pelicula.titulo = peliculaTabla.titulo;
                pelicula.añolanzamiento = peliculaTabla.añolanzamiento;
                pelicula.sinopsis = peliculaTabla.sinopsis;
                pelicula.premios = peliculaTabla.premios;
                pelicula.duracion = peliculaTabla.duracion;
                pelicula.clasificacion = peliculaTabla.clasificacion;
                pelicula.presupuesto = peliculaTabla.presupuesto;
                pelicula.recaudacion = peliculaTabla.recaudacion;

                peliculas.Add(pelicula);
            }
            return peliculas;
        }
        // GET: api/Empleados/5
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult Getpelicula(int id)
        {
            pelicula peliculaTabla = db.pelicula.Find(id);
            if (peliculaTabla == null)
            {
                return NotFound();
            }

            pelicula pelicula = new pelicula();
            pelicula.titulo = peliculaTabla.titulo;
            pelicula.añolanzamiento = peliculaTabla.añolanzamiento;
            pelicula.sinopsis = peliculaTabla.sinopsis;
            pelicula.premios = peliculaTabla.premios;
            pelicula.duracion = peliculaTabla.duracion;
            pelicula.clasificacion = peliculaTabla.clasificacion;
            pelicula.presupuesto = peliculaTabla.presupuesto;
            pelicula.recaudacion = peliculaTabla.recaudacion;


            return Ok(pelicula);
        }



        // GET: api/Empleados/5
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult GetEmpleado(int? id, int? siguiente)
        {
            pelicula pelicula = null;
            if (siguiente == null)
            {
                pelicula = db.pelicula.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == 1)
                {
                    pelicula = db.pelicula.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<pelicula> peliculas = db.pelicula.Where(x => x.id < id.Value).ToList();
                    if (peliculas != null && peliculas.Count() > 0)
                    {
                        int? idPelicula = peliculas.Max(x => x.id);
                        pelicula = db.pelicula.Where(x => x.id == idPelicula.Value).FirstOrDefault();
                    }
                }
            }
            if (pelicula == null)
            {
                pelicula = db.pelicula.Where(x => x.id == id.Value).FirstOrDefault();
            }
            pelicula peliculaTabla = new pelicula();
            peliculaTabla.titulo = pelicula.titulo;
            peliculaTabla.añolanzamiento = pelicula.añolanzamiento;
            peliculaTabla.sinopsis = pelicula.sinopsis;
            peliculaTabla.premios = pelicula.premios;
            peliculaTabla.duracion = pelicula.duracion;
            peliculaTabla.clasificacion = pelicula.clasificacion;
            peliculaTabla.presupuesto = pelicula.presupuesto;
            peliculaTabla.recaudacion = pelicula.recaudacion;

            return Ok(peliculaTabla);

        }
        // PUT: api/Peliculas/5
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

        // POST: api/Peliculas
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

        // DELETE: api/Peliculas/5
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