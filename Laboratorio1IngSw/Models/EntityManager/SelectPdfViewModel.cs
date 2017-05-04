using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1IngSw.Models.EntityManager
{
    public class SelectPdfViewModel
    {
        public int IDGrado { get; set; }
        public int IDAsignatura { get; set; }
        public int IDTema { get; set; }
        public IEnumerable<SelectListItem> ListaGrados { get; set; }
    }
}