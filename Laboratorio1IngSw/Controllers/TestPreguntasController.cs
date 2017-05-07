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
    public class TestPreguntasController : Controller
    {
        private TestPlataformaEntities db = new TestPlataformaEntities();

        // GET: TestPreguntas
        public ActionResult Index(int? page)
        {
            var temas = db.TestPreguntas.Include(t => t.Test).Include(tema =>tema.Test.Temas).OrderBy(x => x.IDTest);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(temas.ToPagedList(pageNumber, pageSize));
        }
        // GET: TestPreguntas/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPreguntas testPreguntas = db.TestPreguntas.Find(id);
            if (testPreguntas == null)
            {
                return HttpNotFound();
            }
            return View(testPreguntas);
        }

        // GET: TestPreguntas/Create

        public ActionResult Create()
        {
            ViewBag.IDTest = new SelectList(db.Test, "IDTest", "IDTest");
            return View();
        }

        // POST: TestPreguntas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "IDPregunta,IDTest,Descripcion")] TestPreguntas testPreguntas)
        {
            if (ModelState.IsValid)
            {
                db.TestPreguntas.Add(testPreguntas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTest = new SelectList(db.Test, "IDTest", "IDTest", testPreguntas.IDTest);
            return View(testPreguntas);
        }

        // GET: TestPreguntas/Edit/5


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPreguntas testPreguntas = db.TestPreguntas.Find(id);
            if (testPreguntas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTest = new SelectList(db.Test, "IDTest", "IDTest", testPreguntas.IDTest);
            return View(testPreguntas);
        }

        // POST: TestPreguntas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "IDPregunta,IDTest,Descripcion")] TestPreguntas testPreguntas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testPreguntas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTest = new SelectList(db.Test, "IDTest", "IDTest", testPreguntas.IDTest);
            return View(testPreguntas);
        }

        // GET: TestPreguntas/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestPreguntas testPreguntas = db.TestPreguntas.Find(id);
            if (testPreguntas == null)
            {
                return HttpNotFound();
            }
            return View(testPreguntas);
        }

        // POST: TestPreguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            TestPreguntas testPreguntas = db.TestPreguntas.Find(id);
            db.TestPreguntas.Remove(testPreguntas);
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
