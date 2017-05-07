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

                                Test test = new Test();
                                test.IDTema = IDTema;

                                var temascontest = Db.Test.Where(o => o.IDTema == test.IDTema);
                                if (temascontest.Any())
                                {
                                    //Ya existe un test para este tema. Hay que eliminarlo primero.
                                }

                                Db.Test.Add(test);
                                Db.SaveChanges();
                                int idTest = test.IDTest;

                                for (int pagina = 1; pagina <= pdf.NumberOfPages; pagina++)
                                {
                                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                                    string textoPagina = PdfTextExtractor.GetTextFromPage(pdf, pagina, strategy);
                                    textoPagina = Regex.Replace(textoPagina, @"\t|\n|\r", "");

                                    if (textoPagina.Contains("– Test"))
                                    {
                                        lineaspdfparcial = Regex.Split(textoPagina, @"[\d]{1,2}[\.]", RegexOptions.None).ToList();
                                        lineaspdfparcial.RemoveAt(0);
                                        lineaspdfdefinitivo.AddRange(lineaspdfparcial);
                                    }
                                }
                                pdf.Close();

                                for (int bloque = 0, lenpreguntas = lineaspdfdefinitivo.Count; bloque < lenpreguntas; bloque++)
                                {
                                    var preguntaslst = Regex.Split(lineaspdfdefinitivo[bloque], @"([:][\s]*[\s+][\s+/g[A-Z][\.]|[\.][\s][A-Z][\.])", RegexOptions.ExplicitCapture);

                                    if (preguntaslst.Count() > 0)
                                    {
                                        TestPreguntas pregunta = new TestPreguntas();
                                        pregunta.IDTest = idTest;
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
                            catch (Exception ex)
                            {
                                t.Dispose();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return RedirectToAction("Index", "TestRespuestas");
        }
    }
}