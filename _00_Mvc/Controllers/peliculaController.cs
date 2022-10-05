using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _04_Data.Datos;

namespace _00_Mvc.Controllers
{
    public class peliculaController : Controller
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: pelicula
        public ActionResult Index()
        {
            var pelicula = db.pelicula.Include(p => p.actor).Include(p => p.companyia).Include(p => p.director).Include(p => p.genero);
            return View(pelicula.ToList());
        }

        // GET: pelicula/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    pelicula pelicula = db.pelicula.Find(id);
        //    if (pelicula == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pelicula);
        //}
        public ActionResult Details(int? id, bool? siguiente)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = null;
            if (siguiente == null)
            {
                pelicula = db.pelicula.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == true)
                {
                    pelicula = db.pelicula.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<pelicula> peliculas = db.pelicula.Where(x => x.id < id.Value).ToList();
                    if (peliculas != null && peliculas.Count() > 0)
                    {
                        int? idpelicula = peliculas.Max(x => x.id);
                        pelicula = db.pelicula.Where(x => x.id == idpelicula.Value).FirstOrDefault();
                    }
                }
            }
            if (pelicula == null)
            {
                pelicula = db.pelicula.Where(x => x.id == id.Value).FirstOrDefault();
            }
            return View(pelicula);
        }


        // GET: pelicula/Create
        public ActionResult Create()
        {
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal");
            ViewBag.id_companyia = new SelectList(db.companyia, "id", "nombre");
            ViewBag.id_director = new SelectList(db.director, "id", "nombre");
            ViewBag.id_genero = new SelectList(db.genero, "id", "nombre");
            return View();
        }

        // POST: pelicula/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_actor,id_director,id_genero,id_companyia,titulo,añolanzamiento,sinopsis,premios,duracion,clasificacion,presupuesto,recaudacion")] pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.pelicula.Add(pelicula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", pelicula.id_actor);
            ViewBag.id_companyia = new SelectList(db.companyia, "id", "nombre", pelicula.id_companyia);
            ViewBag.id_director = new SelectList(db.director, "id", "nombre", pelicula.id_director);
            ViewBag.id_genero = new SelectList(db.genero, "id", "nombre", pelicula.id_genero);
            return View(pelicula);
        }

        // GET: pelicula/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", pelicula.id_actor);
            ViewBag.id_companyia = new SelectList(db.companyia, "id", "nombre", pelicula.id_companyia);
            ViewBag.id_director = new SelectList(db.director, "id", "nombre", pelicula.id_director);
            ViewBag.id_genero = new SelectList(db.genero, "id", "nombre", pelicula.id_genero);
            return View(pelicula);
        }

        // POST: pelicula/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_actor,id_director,id_genero,id_companyia,titulo,añolanzamiento,sinopsis,premios,duracion,clasificacion,presupuesto,recaudacion")] pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelicula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", pelicula.id_actor);
            ViewBag.id_companyia = new SelectList(db.companyia, "id", "nombre", pelicula.id_companyia);
            ViewBag.id_director = new SelectList(db.director, "id", "nombre", pelicula.id_director);
            ViewBag.id_genero = new SelectList(db.genero, "id", "nombre", pelicula.id_genero);
            return View(pelicula);
        }

        // GET: pelicula/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: pelicula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pelicula pelicula = db.pelicula.Find(id);
            db.pelicula.Remove(pelicula);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public ActionResult _peliculaPartialView(pelicula Pelicula)
        {
            return View("_peliculaPartialView", Pelicula);
        }

    }
}
