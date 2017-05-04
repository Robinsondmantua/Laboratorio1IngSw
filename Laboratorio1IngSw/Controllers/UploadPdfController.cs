using Laboratorio1IngSw.Models.DB;
using Laboratorio1IngSw.Models.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1IngSw.Controllers
{
    public class UploadPdfController : Controller
    {
        // GET: UploadPdf
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadPdf()
        {
            TestPlataformaEntities modelo = new TestPlataformaEntities();

            var consulta = modelo.Grado.Select(c => new SelectListItem
            {
                Value = c.IDGrado.ToString(),
                Text = c.Descripcion,
                Selected = c.IDGrado.Equals(1)  
            });

            var grados = new SelectPdfViewModel
            {
                ListaGrados = consulta.AsEnumerable()
            };

            return PartialView(grados);
        }

        [HttpPost]
        public ActionResult UploadTemario(HttpPostedFileBase file)
        {
            try
            {
                if(file.ContentLength > 0)
                {

                }
            }
            catch (Exception ex)
            {
                
            }
            return View();
        }
    }
}