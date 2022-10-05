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
    public class FraseMvcController : Controller
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: FraseMvc
        public ActionResult Index()
        {
            var frase = db.frase.Include(f => f.actor).Include(f => f.pelicula);
            return View(frase.ToList());
        }

        // GET: FraseMvc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            frase frase = db.frase.Find(id);
            if (frase == null)
            {
                return HttpNotFound();
            }
            return View(frase);
        }

        // GET: FraseMvc/Create
        public ActionResult Create()
        {
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal");
            ViewBag.id_pelicula = new SelectList(db.pelicula, "id", "titulo");
            return View();
        }

        // POST: FraseMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_pelicula,id_actor,frase1")] frase frase)
        {
            if (ModelState.IsValid)
            {
                db.frase.Add(frase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", frase.id_actor);
            ViewBag.id_pelicula = new SelectList(db.pelicula, "id", "titulo", frase.id_pelicula);
            return View(frase);
        }

        // GET: FraseMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            frase frase = db.frase.Find(id);
            if (frase == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", frase.id_actor);
            ViewBag.id_pelicula = new SelectList(db.pelicula, "id", "titulo", frase.id_pelicula);
            return View(frase);
        }

        // POST: FraseMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_pelicula,id_actor,frase1")] frase frase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", frase.id_actor);
            ViewBag.id_pelicula = new SelectList(db.pelicula, "id", "titulo", frase.id_pelicula);
            return View(frase);
        }

        // GET: FraseMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            frase frase = db.frase.Find(id);
            if (frase == null)
            {
                return HttpNotFound();
            }
            return View(frase);
        }

        // POST: FraseMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            frase frase = db.frase.Find(id);
            db.frase.Remove(frase);
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
    }
}
