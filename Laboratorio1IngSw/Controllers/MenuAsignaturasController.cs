using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1IngSw.Controllers
{
    public class MenuAsignaturasController : Controller
    {
        // GET: MenuAsignaturas
        [Authorize]
        public ActionResult MenuAsignaturas()
        {
            return View();
        }
    }
}