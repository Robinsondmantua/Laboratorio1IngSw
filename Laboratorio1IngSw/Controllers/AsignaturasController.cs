using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Laboratorio1IngSw.Models.DB;
using PagedList;

namespace Laboratorio1IngSw.Controllers
{
    public class AsignaturasController : Controller
    {
        private TestPlataformaEntities db = new TestPlataformaEntities();

        // GET: Asignaturas

        public ActionResult Index(int? page)
        {
            var asignaturas = db.Asignaturas.Include(a => a.Grado).OrderBy(x => x.IDAsignatura);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(asignaturas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Asignaturas/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignaturas asignaturas = db.Asignaturas.Find(id);
            if (asignaturas == null)
            {
                return HttpNotFound();
            }
            return View(asignaturas);
        }

        // GET: Asignaturas/Create

        public ActionResult Create()
        {
            ViewBag.IDGrado = new SelectList(db.Grado, "IDGrado", "Descripcion");
            return View();
        }

        // POST: Asignaturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAsignatura,IDGrado,Descripcion")] Asignaturas asignaturas)
        {
            if (ModelState.IsValid)
            {
                db.Asignaturas.Add(asignaturas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDGrado = new SelectList(db.Grado, "IDGrado", "Descripcion", asignaturas.IDGrado);
            return View(asignaturas);
        }

        // GET: Asignaturas/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignaturas asignaturas = db.Asignaturas.Find(id);
            if (asignaturas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDGrado = new SelectList(db.Grado, "IDGrado", "Descripcion", asignaturas.IDGrado);
            return View(asignaturas);
        }

        // POST: Asignaturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "IDAsignatura,IDGrado,Descripcion")] Asignaturas asignaturas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asignaturas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDGrado = new SelectList(db.Grado, "IDGrado", "Descripcion", asignaturas.IDGrado);
            return View(asignaturas);
        }

        // GET: Asignaturas/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asignaturas asignaturas = db.Asignaturas.Find(id);
            if (asignaturas == null)
            {
                return HttpNotFound();
            }
            return View(asignaturas);
        }

        // POST: Asignaturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Asignaturas asignaturas = db.Asignaturas.Find(id);
            db.Asignaturas.Remove(asignaturas);
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
