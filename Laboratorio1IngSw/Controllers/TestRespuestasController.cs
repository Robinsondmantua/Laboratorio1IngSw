using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Laboratorio1IngSw.Models.DB;

namespace Laboratorio1IngSw.Controllers
{
    public class TestRespuestasController : Controller
    {
        private TestPlataformaEntities db = new TestPlataformaEntities();

        // GET: TestRespuestas
        public ActionResult Index()
        {
            var testRespuestas = db.TestRespuestas.Include(t => t.TestPreguntas);
            return View(testRespuestas.ToList());
        }

        // GET: TestRespuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRespuestas testRespuestas = db.TestRespuestas.Find(id);
            if (testRespuestas == null)
            {
                return HttpNotFound();
            }
            return View(testRespuestas);
        }

        // GET: TestRespuestas/Create
        public ActionResult Create()
        {
            ViewBag.IDPregunta = new SelectList(db.TestPreguntas, "IDPregunta", "Descripcion");
            return View();
        }

        // POST: TestRespuestas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTestRespuestas,IDTest,Descripcion,Orden,Correcta,IDPregunta")] TestRespuestas testRespuestas)
        {
            if (ModelState.IsValid)
            {
                db.TestRespuestas.Add(testRespuestas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDPregunta = new SelectList(db.TestPreguntas, "IDPregunta", "Descripcion", testRespuestas.IDPregunta);
            return View(testRespuestas);
        }

        // GET: TestRespuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRespuestas testRespuestas = db.TestRespuestas.Find(id);
            if (testRespuestas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPregunta = new SelectList(db.TestPreguntas, "IDPregunta", "Descripcion", testRespuestas.IDPregunta);
            return View(testRespuestas);
        }

        // POST: TestRespuestas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTestRespuestas,IDTest,Descripcion,Orden,Correcta,IDPregunta")] TestRespuestas testRespuestas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testRespuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDPregunta = new SelectList(db.TestPreguntas, "IDPregunta", "Descripcion", testRespuestas.IDPregunta);
            return View(testRespuestas);
        }

        // GET: TestRespuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestRespuestas testRespuestas = db.TestRespuestas.Find(id);
            if (testRespuestas == null)
            {
                return HttpNotFound();
            }
            return View(testRespuestas);
        }

        // POST: TestRespuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestRespuestas testRespuestas = db.TestRespuestas.Find(id);
            db.TestRespuestas.Remove(testRespuestas);
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
