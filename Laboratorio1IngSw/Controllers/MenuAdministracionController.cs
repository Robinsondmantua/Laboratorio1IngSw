using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1IngSw.Controllers
{
    [Authorize]
    public class MenuAdministracionController : Controller
    {
        // GET: MenuAdministracion
        public ActionResult MenuAdministracion()
        {
            return View();
        }
    }
}