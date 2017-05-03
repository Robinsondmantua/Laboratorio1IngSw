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
    public class TestTemasController : Controller
    {
        private TestPlataformaEntities db = new TestPlataformaEntities();

        // GET: TestTemas
        public ActionResult Index()
        {
            var testTemas = db.TestTemas.Include(t => t.Temas);
            return View(testTemas.ToList());
        }

        // GET: TestTemas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTemas testTemas = db.TestTemas.Find(id);
            if (testTemas == null)
            {
                return HttpNotFound();
            }
            return View(testTemas);
        }

        // GET: TestTemas/Create
        public ActionResult Create()
        {
            ViewBag.IDTema = new SelectList(db.Temas, "IDTema", "Descripcion");
            return View();
        }

        // POST: TestTemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTest,IDTema,Descripcion,Orden")] TestTemas testTemas)
        {
            if (ModelState.IsValid)
            {
                db.TestTemas.Add(testTemas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDTema = new SelectList(db.Temas, "IDTema", "Descripcion", testTemas.IDTema);
            return View(testTemas);
        }

        // GET: TestTemas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTemas testTemas = db.TestTemas.Find(id);
            if (testTemas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTema = new SelectList(db.Temas, "IDTema", "Descripcion", testTemas.IDTema);
            return View(testTemas);
        }

        // POST: TestTemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTest,IDTema,Descripcion,Orden")] TestTemas testTemas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testTemas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTema = new SelectList(db.Temas, "IDTema", "Descripcion", testTemas.IDTema);
            return View(testTemas);
        }

        // GET: TestTemas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestTemas testTemas = db.TestTemas.Find(id);
            if (testTemas == null)
            {
                return HttpNotFound();
            }
            return View(testTemas);
        }

        // POST: TestTemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestTemas testTemas = db.TestTemas.Find(id);
            db.TestTemas.Remove(testTemas);
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
