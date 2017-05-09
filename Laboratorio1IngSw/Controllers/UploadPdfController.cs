using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Laboratorio1IngSw.Models.DB;
using Laboratorio1IngSw.Models.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Laboratorio1IngSw.Controllers
{
    [Authorize]
    public class UploadPdfController : Controller
    {
        private TestPlataformaEntities Db = new TestPlataformaEntities();
        // GET: UploadPdf
        public ActionResult UploadPdf(int id)
        {
            var temas = Db.Temas.Find(id);
            return View(temas);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadPdf(HttpPostedFileBase file, int IDTema)
        {
            if (ModelState.IsValid)
            {
                ViewBag.DatosGrabados = false;
                try
                {
                    if (file.ContentLength > 0)
                    {

                        using (TransactionScope t = new TransactionScope())
                        {
                            try
                            {
                                PdfReader pdf = new PdfReader(file.InputStream);
                                List<string> lineaspdfparcial = new List<string>();
                                List<string> lineaspdfdefinitivo = new List<string>();
                                // Valida si ya existe un test para el tema seleccionado.
                                var temascontest = Db.TestPreguntas.Where(o => o.IDTema == IDTema);
                                if (temascontest.Any())
                                {
                                    //Ya existe un test para este tema. Hay que eliminar las pregutnas
                                    //asociadas al tema.
                                    ModelState.AddModelError("", "Ya existe un test asociado a este tema. Elimina las preguntas.");
                                }
                                else
                                {
                                    for (int pagina = 1; pagina <= pdf.NumberOfPages; pagina++)
                                    {
                                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                                        string textoPagina = PdfTextExtractor.GetTextFromPage(pdf, pagina, strategy);
                                        textoPagina = Regex.Replace(textoPagina, @"\t|\n|\r", "");

                                        // Si coincide la búsqueda, recupera las líneas que coinciden con la 
                                        // expresión regular (aquellas que comiencen con uno o dos dígitos seguidos
                                        // de un punto).
                                        if (textoPagina.Contains("– Test"))
                                        {
                                            lineaspdfparcial = Regex.Split(textoPagina, @"[\d]{1,2}[\.]", RegexOptions.None).ToList();
                                            lineaspdfparcial.RemoveAt(0);
                                            lineaspdfdefinitivo.AddRange(lineaspdfparcial);
                                        }
                                    }
                                    pdf.Close();

                                    //Recupera las respuestas partiendo de las líneas anteriores e inserta 
                                    //preguntas y respuestas en la BB.DD, completando la transacción.
                                    for (int bloque = 0, lenpreguntas = lineaspdfdefinitivo.Count; bloque < lenpreguntas; bloque++)
                                    {
                                        var preguntaslst = Regex.Split(lineaspdfdefinitivo[bloque], @"([:][\s]*[\s+][\s+/g[A-Z][\.]|[\.][\s]*[A-Z][\.])", RegexOptions.ExplicitCapture);

                                        if (preguntaslst.Count() > 0)
                                        {
                                            TestPreguntas pregunta = new TestPreguntas();
                                            pregunta.IDTema = IDTema;
                                            pregunta.Descripcion = preguntaslst.ToList().FirstOrDefault();

                                            Db.TestPreguntas.Add(pregunta);
                                            Db.SaveChanges();
                                            int idPregunta = pregunta.IDPregunta;

                                            short intOrden = 1;
                                            List<TestRespuestas> respuestas = (from r in preguntaslst.Skip(1).Take(preguntaslst.Count())
                                                                               select new TestRespuestas
                                                                               {
                                                                                   IDPregunta = idPregunta,
                                                                                   Descripcion = r,
                                                                                   Orden = intOrden++,
                                                                                   Correcta = false
                                                                               }).ToList();
                                            foreach (TestRespuestas respuesta in respuestas)
                                            {
                                                Db.TestRespuestas.Add(respuesta);
                                                Db.SaveChanges();
                                            }
                                        }
                                    }
                                    t.Complete();
                                    ViewBag.DatosGrabados = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("", "Error al importar el test.");
                                t.Dispose();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return RedirectToAction("Index", "TestRespuestas");
        }
    }
}