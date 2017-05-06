using Laboratorio1IngSw.Models.DB;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1IngSw.Controllers
{
    public class PrevisualizarPdfController : Controller
    {
        // GET: PrevisualizarPdf
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Previsualizar(TestPreguntas tema, int? page)
        {
            if (page== null)
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(tema.TestRespuestas.ToPagedList(pageNumber, pageSize));
        }
    }
}