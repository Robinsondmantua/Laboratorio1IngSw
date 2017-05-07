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
    [Authorize]
    public class TemasController : Controller
    {
        private TestPlataformaEntities db = new TestPlataformaEntities();

        // GET: Temas

        public ActionResult Index(int? page)
        {
            var temas = db.Temas.Include(t => t.Asignaturas).OrderBy(x => x.IDAsignatura);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(temas.ToPagedList(pageNumber, pageSize));
        }
        // GET: Temas/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = db.Temas.Find(id);
            if (temas == null)
            {
                return HttpNotFound();
            }
            return View(temas);
        }

        // GET: Temas/Create

        public ActionResult Create()
        {
            ViewBag.IDAsignatura = new SelectList(db.Asignaturas, "IDAsignatura", "Descripcion");
            return View();
        }

        // POST: Temas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "IDTema,IDAsignatura,Descripcion")] Temas temas)
        {
            if (ModelState.IsValid)
            {
                db.Temas.Add(temas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDAsignatura = new SelectList(db.Asignaturas, "IDAsignatura", "Descripcion", temas.IDAsignatura);
            return View(temas);
        }

        // GET: Temas/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = db.Temas.Find(id);
            if (temas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDAsignatura = new SelectList(db.Asignaturas, "IDAsignatura", "Descripcion", temas.IDAsignatura);
            return View(temas);
        }

        // POST: Temas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "IDTema,IDAsignatura,Descripcion")] Temas temas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(temas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDAsignatura = new SelectList(db.Asignaturas, "IDAsignatura", "Descripcion", temas.IDAsignatura);
            return View(temas);
        }

        // GET: Temas/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temas temas = db.Temas.Find(id);
            if (temas == null)
            {
                return HttpNotFound();
            }
            return View(temas);
        }

        // POST: Temas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Temas temas = db.Temas.Find(id);
            db.Temas.Remove(temas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UploadPdf(int idtema)
        {
            return RedirectToAction("UploadPdf", "UploadPdf", idtema);
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
