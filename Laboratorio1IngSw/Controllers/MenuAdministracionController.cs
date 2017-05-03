using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1IngSw.Controllers
{
    public class MenuAdministracionController : Controller
    {
        // GET: MenuAdministracion
        [Authorize]
        public ActionResult MenuAdministracion()
        {
            return View();
        }
    }
}