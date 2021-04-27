using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using AWGestion.Models;


using System;
using System.Data;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Diagnostics;
using Microsoft.SqlServer.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AWGestion.Controllers
{
    [Authorize(Roles = "PM, Admin")]
    public class AlmacenController : Controller
    {
        private GestionContext _context;
        
        public AlmacenController(GestionContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "PM, Admin")]
        public IActionResult Informacion()
        {
            ViewBag.Context = _context;
            RegistraBitacora("Informacion", "Consulta");
            return View(_context.Proyectos.ToList());
        }

        [Authorize(Roles = "PM, Admin")]
        [Route("Almacen/Salidas/VerSalidas/{proyectoId}")]
        public IActionResult VerSalidas(int proyectoId)
        {
            ViewBag.Context = _context;
            ViewBag.ProyectoId = proyectoId;
            List<Salida> salidas = _context.Salidas.Where(salida => salida.ProyectoId == proyectoId).ToList();
            return View("Salidas/VerSalidas", salidas);
        }

        [Authorize(Roles = "PM, Admin")]
        [Route("Almacen/Salidas/AgregarSalidas/{proyectoId}")]
        public IActionResult AgregarSalidas(int proyectoId)
        {
            ViewBag.ProyectoId = proyectoId;
            var salidas = _context.Salidas.Where(sa => sa.ProyectoId == proyectoId);
            List<string> salidasModels = salidas.Select(s => s.ArtModelo).ToList();
            var invArticulosList = new List<SelectListItem>();
            var invArticulos = _context.InvArticulos.Where(x => !salidasModels.Contains(x.ArtModelo)).ToList();
            var catArticulos = _context.CatArticulos.ToList();
            foreach (var articulo in catArticulos)
            {
                foreach (var elemento in invArticulos)
                {
                    if (elemento.ArtId == articulo.ArtId)
                    {
                        if (elemento.CantidadEnAlmacen > 0)
                        {
                            invArticulosList.Add(new SelectListItem() {Text = elemento.ArtModelo, Value = elemento.ArtModelo + " " + articulo.ArtNombre});
                        }
                    }
                }
            }
            ViewBag.Articulos = invArticulosList;
            ViewBag.Context = _context;
            return View("Salidas/AgregarSalidas");
        }

        [Authorize(Roles = "PM, Admin")]
        [HttpPost]
        [Route("Almacen/Salidas/AgregarSalidas/{proyectoId}")]
        public IActionResult AgregarSalidas(Salida salida, int proyectoId)
        {
            var existencia = _context.InvArticulos
                .Where(x => x.ArtModelo == salida.ArtModelo)
                .Select(x => x.CantidadEnAlmacen)
                .ToArray();
            
            salida.Fecha = DateTime.Now;

            if (ModelState.IsValid)
            {
                if ((existencia[0] < salida.Cantidad))
                {
                    ModelState.AddModelError(string.Empty, "La cantidad solicitada excede la del inventario");
                    ViewBag.ProyectoId = proyectoId;
                    List<string> salidasModels = _context.Salidas.Where(sa => sa.ProyectoId == proyectoId)
                        .Select(s => s.ArtModelo).ToList();
                    var invArticulosList = new List<SelectListItem>();
                    var invArticulos = _context.InvArticulos.Where(x => !salidasModels.Contains(x.ArtModelo)).ToList();
                    var catArticulos = _context.CatArticulos.ToList();

                    foreach (var articulo in catArticulos)
                    {
                        foreach (var elemento in invArticulos)
                        {
                            if (elemento.ArtId == articulo.ArtId)
                            {
                                if (elemento.CantidadEnAlmacen > 0)
                                {
                                    invArticulosList.Add(new SelectListItem()
                                    {
                                        Text = elemento.ArtModelo, Value = elemento.ArtModelo + " " + articulo.ArtNombre
                                    });
                                }
                            }
                        }
                    }

                    ViewBag.Articulos = invArticulosList;
                    ViewBag.Context = _context;
                    return View("Salidas/AgregarSalidas", salida);
                }

                ViewBag.Context = _context;
                ViewBag.ProyectoId = salida.ProyectoId;
                _context.Salidas.Add(salida);
                _context.SaveChanges();
                ActualizaSalidaStock(salida.ArtModelo, salida.Cantidad);
                RegistraBitacora("Salidas", "Inserción");
                List<Salida> salidas = _context.Salidas.Where(salidaOut => salidaOut.ProyectoId == salida.ProyectoId)
                    .ToList();

                return View("Salidas/VerSalidas", salidas);
            }
            
            return View("Salidas/AgregarSalidas");
        }

        [Authorize(Roles = "PM, Admin")]
        [Route("Almacen/Entradas/VerEntradas/{proyectoId}")]
        public IActionResult VerEntradas(int proyectoId)
        {
            ViewBag.Context = _context;
            ViewBag.ProyectoId = proyectoId;
            List<Entrada> entradas = _context.Entradas.Where(entrada => entrada.ProyectoId == proyectoId).ToList();
            return View("Entradas/VerEntradas", entradas);
        }

        [Authorize(Roles = "PM, Admin")]
        [Route("Almacen/Entradas/AgregarEntradas/{proyectoId}")]
        public IActionResult AgregarEntradas(int proyectoId)
        {
            ViewBag.ProyectoId = proyectoId;
            ViewBag.Fecha = DateTime.Now;
            var salidas = _context.Salidas.Where(en => en.ProyectoId == proyectoId);
            var entradas = _context.Entradas.Where(en => en.ProyectoId == proyectoId);
            List<string> entradasModels = entradas.Select(e => e.ArtModelo).ToList();
            List<string> salidasModels = salidas.Select(s => s.ArtModelo).ToList();
            var invArticulosList = new List<SelectListItem>();
            var invArticulos1 = _context.InvArticulos.Where(x => !entradasModels.Contains(x.ArtModelo)).ToList();
            var invArticulos2 = _context.InvArticulos.Where(x => salidasModels.Contains(x.ArtModelo)).ToList();
            var invArticulos = invArticulos1.Where(x => invArticulos2.Contains(x)).ToList();
            var catArticulos = _context.CatArticulos.ToList();
            foreach (var articulo in catArticulos)
            {
                foreach (var elemento in invArticulos)
                {
                    if (elemento.ArtId == articulo.ArtId)
                    {
                        if (elemento.CantidadPrestada > 0)
                        {
                            invArticulosList.Add(new SelectListItem() {Text = elemento.ArtModelo, Value = elemento.ArtModelo + " " + articulo.ArtNombre});
                        }
                    }
                }
            }
            ViewBag.Articulos = invArticulosList;
            ViewBag.Context = _context;
            return View("Entradas/AgregarEntradas");
        }

        [Authorize(Roles = "PM, Admin")]
        [HttpPost]
        [Route("Almacen/Entradas/AgregarEntradas/{proyectoId}")]
        public IActionResult AgregarEntradas(Entrada entrada, int proyectoId)
        {
            var prestado = _context.Salidas
                .Where(p => p.ArtModelo == entrada.ArtModelo)
                .Select(p => p.Cantidad)
                .ToArray();
            
            entrada.Fecha = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (entrada.Cantidad > prestado[0])
                {
                    ModelState.AddModelError(string.Empty,
                        "La cantidad que eligió devolver, excede de la cantidad prestada");
                    ViewBag.ProyectoId = proyectoId;
                    ViewBag.Fecha = DateTime.Now;
                    var salidas = _context.Salidas.Where(en => en.ProyectoId == proyectoId);
                    var ents = _context.Entradas.Where(en => en.ProyectoId == proyectoId);
                    List<string> entradasModels = ents.Select(e => e.ArtModelo).ToList();
                    List<string> salidasModels = salidas.Select(s => s.ArtModelo).ToList();
                    var invArticulosList = new List<SelectListItem>();
                    var invArticulos1 = _context.InvArticulos.Where(x => !entradasModels.Contains(x.ArtModelo)).ToList();
                    var invArticulos2 = _context.InvArticulos.Where(x => salidasModels.Contains(x.ArtModelo)).ToList();
                    var invArticulos = invArticulos1.Where(x => invArticulos2.Contains(x)).ToList();
                    var catArticulos = _context.CatArticulos.ToList();
                    foreach (var articulo in catArticulos)
                    {
                        foreach (var elemento in invArticulos)
                        {
                            if (elemento.ArtId == articulo.ArtId)
                            {
                                if (elemento.CantidadPrestada > 0)
                                {
                                    invArticulosList.Add(new SelectListItem() {Text = elemento.ArtModelo, Value = elemento.ArtModelo + " " + articulo.ArtNombre});
                                }
                            }
                        }
                    }
                    ViewBag.Articulos = invArticulosList;
                    ViewBag.Context = _context;
                    return View("Entradas/AgregarEntradas", entrada);
                }

                ViewBag.Context = _context;
                ViewBag.ProyectoId = entrada.ProyectoId;
                _context.Entradas.Add(entrada);
                _context.SaveChanges();
                ActualizaEntradaStock(entrada.ArtModelo, entrada.Cantidad);
                RegistraBitacora("Salidas", "Inserción");
                List<Entrada> entradas = _context.Entradas
                    .Where(entradaOut => entradaOut.ProyectoId == entrada.ProyectoId).ToList();
                return View("Entradas/VerEntradas", entradas);
            }
            return View("Entradas/AgregarEntradas");
        }

        [Authorize]
        public void ExecuteQuery(string query)
        {
            SqlConnection conection = new SqlConnection("Server= localhost; Database= webstore; Integrated Security=SSPI; Server=localhost\\sqlexpress;");
            conection.Open();
            SqlCommand command = new SqlCommand(query,conection); // Create a object of SqlCommand class
            command.ExecuteNonQuery();
            conection.Close();
        }

        [Authorize]
        public void RegistraBitacora(string tabla, string operacion)
        {
            ExecuteQuery($"exec RegistraBitacora {tabla}, {operacion}");
        }

        [Authorize(Roles = "Admin")]
        public void ActualizaSalidaStock(string artModelo, int cantidad)
        {
            string artModeloRender = "'" + artModelo + "'";
            ExecuteQuery($"exec ActualizaSalidaStock {artModeloRender}, {cantidad}");
        }

        [Authorize(Roles = "Admin")]
        public void ActualizaEntradaStock(string artModelo, int cantidad)
        {
            string artModeloRender = "'" + artModelo + "'";
            ExecuteQuery($"exec ActualizaEntradaStock {artModeloRender}, {cantidad}");
        }

        [Authorize(Roles = "PM, Admin")]
        [Route("Almacen/GenerarReporteSalida/{proyectoId}")]
        public IActionResult GenerarReportesSalida(int proyectoId)
        {
            string pathdb = _context.Parametros.FirstOrDefault(parametro => parametro.VcParamName == "PathBuildPdf")?.VcParamValue;
            ViewBag.Context = _context;
            GeneratePdfFile(proyectoId, "Salida", pathdb);
            TempData["PDFGenerado"] = $"Archivo de reporte de salida generado correctamente en la ruta {pathdb}!";
            return View("Informacion", _context.Proyectos.ToList());
        }

        [Authorize(Roles = "PM, Admin")]
        [Route("Almacen/GenerarReporteEntrada/{proyectoId}")]
        public IActionResult GenerarReportesEntrada(int proyectoId)
        {
            string pathdb = _context.Parametros.FirstOrDefault(parametro => parametro.VcParamName == "PathBuildPdf")?.VcParamValue;
            ViewBag.Context = _context;
            GeneratePdfFile(proyectoId, "Entrada", pathdb);
            TempData["PDFGenerado"] = $"Archivo de reporte de entrada generado correctamente en la ruta {pathdb}!";
            return View("Informacion", _context.Proyectos.ToList());
        }

        [Authorize(Roles = "PM, Admin")]
        protected void GeneratePdfFile(int proyectoId, string opt, string pathdb)
        {
            string path = (opt == "Salida")? $@"{pathdb}Reporte-Salida-{DateTime.Now.ToString("dd-MM-yyyy")}_proyecto_{proyectoId}.pdf" : $@"{pathdb}Reporte-Entrada-{DateTime.Now.ToString("dd-MM-yyyy")}_proyecto_{proyectoId}.pdf";
            //Create document  
            Document doc = new Document();
            //Create PDF Table  
            PdfPTable tableLayout = new PdfPTable(6);
            //Create a PDF file in specific path  
            PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
            //Open the PDF document  
            doc.Open();
            //Add Content to PDF  
            doc.Add(Add_Content_To_PDF(tableLayout, proyectoId, opt));
            // Closing the document  
            doc.Close();
        }

        [Authorize(Roles = "PM, Admin")]
        private PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, int proyectoId, string opt)
        {
            float[] headers =
            {
                20, 20, 20, 20, 20, 20
            }; //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 80; //Set the PDF File witdh percentage  
            Proyecto proyecto = _context.Proyectos.Find(proyectoId);
            Empresa empresa = _context.Empresas.Find(proyecto.EmpresaRfc);
            List<Salida> salidas = _context.Salidas.Where(salida => salida.ProyectoId == proyectoId).ToList();
            List<string> salidasModels = salidas.Select(s => s.ArtModelo).ToList();
            List<InvArticulo> invArticulos = _context.InvArticulos.Where(x => salidasModels.Contains(x.ArtModelo)).ToList();
            List<int> invArtIds = invArticulos.Select(s => s.ArtId).ToList();
            List<CatArticulo> catArticulos = _context.CatArticulos.Where(x => invArtIds.Contains(x.ArtId)).ToList();
            List<CatTipoArt> catTipoArts = _context.CatTipoArts.ToList();
            AspNetUser user = _context.AspNetUsers.Find(proyecto.UserId);
            /******ENTRADA******/
            List<Entrada> entradas = _context.Entradas.Where(entrada => entrada.ProyectoId == proyectoId).ToList();
            List<string> entradasModels = entradas.Select(e => e.ArtModelo).ToList();
            List<InvArticulo> eInvArticulos = _context.InvArticulos.Where(x => entradasModels.Contains(x.ArtModelo)).ToList();

            string titulo = (opt == "Salida") ? "Reporte de salida de material" : "Reporte de entrada de material";
            string intro = (opt == "Salida") ? $"Por medio de la presente, se autoriza a {user.Nombres} {user.ApPat} {user.ApMat} con el RFC {user.RFC} para sacar la siguiente lista de materiales para la empresa {empresa.EmpresaRazSoc} con el RFC {empresa.EmpresaRfc} con dirección en {empresa.EmpresaCalle} No. {empresa.EmpresaNumExt} Int. {empresa.EmpresaNumInt} Ciudad {empresa.EmpresaCd} C.P. {empresa.EmpresaCp} en {empresa.EmpresaEdo}." : $"Por medio de la presente, se autoriza a {user.Nombres} {user.ApPat} {user.ApMat} con el RFC {user.RFC} para ingresar la siguiente lista de materiales provenientes de la empresa {empresa.EmpresaRazSoc} con el RFC {empresa.EmpresaRfc} con dirección en {empresa.EmpresaCalle} No. {empresa.EmpresaNumExt} Int. {empresa.EmpresaNumInt} Ciudad {empresa.EmpresaCd} C.P. {empresa.EmpresaCp} en {empresa.EmpresaEdo}.";
            
            //Add Title to the PDF file at the top 
            tableLayout.AddCell(
                new PdfPCell(new Paragraph(titulo, new Font(Font.FontFamily.HELVETICA, 13, 1)))
                {
                    Colspan = 6,
                    Border = 0,
                    PaddingBottom = 20,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });

            tableLayout.AddCell(
                new PdfPCell(new Phrase(intro, new Font(Font.FontFamily.HELVETICA, 8, 1)))
                {
                    Colspan = 12,
                    Border = 0,
                    PaddingBottom = 20,
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED
                });
            //Add header  
            AddCellToHeader(tableLayout, "Modelo");
            AddCellToHeader(tableLayout, "Artículo");
            AddCellToHeader(tableLayout, "Marca");
            AddCellToHeader(tableLayout, "Tipo de Artículo");
            AddCellToHeader(tableLayout, "Cantidad");
            AddCellToHeader(tableLayout, "Fecha");
            if (opt == "Salida")
            {
                foreach (Salida salida in salidas)
                {
                    foreach (InvArticulo invArticulo in invArticulos)
                    {
                        foreach (CatArticulo catArticulo in catArticulos)
                        {
                            foreach (CatTipoArt catTipoArt in catTipoArts)
                            {
                                if (salida.ArtModelo == invArticulo.ArtModelo && invArticulo.ArtId == catArticulo.ArtId && catArticulo.TipoArtId == catTipoArt.TipoArtId)
                                {
                                    Marca marca = _context.Marcas.Find(catArticulo.MarcaId);
                                    AddCellToBody(tableLayout, salida.ArtModelo);
                                    AddCellToBody(tableLayout, catArticulo.ArtNombre);
                                    AddCellToBody(tableLayout, marca.VcMarcaName);
                                    AddCellToBody(tableLayout, catTipoArt.TipoArtDesc);
                                    AddCellToBody(tableLayout, salida.Cantidad.ToString());
                                    AddCellToBody(tableLayout, salida.Fecha.ToShortDateString());
                                }
                            }
                        }
                    }
                }
            }
            else {
                foreach (Entrada entrada in entradas)
                {
                    foreach (InvArticulo invArticulo in invArticulos)
                    {
                        foreach (CatArticulo catArticulo in catArticulos)
                        {
                            foreach (CatTipoArt catTipoArt in catTipoArts)
                            {
                                if (entrada.ArtModelo == invArticulo.ArtModelo && invArticulo.ArtId == catArticulo.ArtId && catArticulo.TipoArtId == catTipoArt.TipoArtId)
                                {
                                    Marca marca = _context.Marcas.Find(catArticulo.MarcaId);
                                    AddCellToBody(tableLayout, entrada.ArtModelo);
                                    AddCellToBody(tableLayout, catArticulo.ArtNombre);
                                    AddCellToBody(tableLayout, marca.VcMarcaName);
                                    AddCellToBody(tableLayout, catTipoArt.TipoArtDesc);
                                    AddCellToBody(tableLayout, entrada.Cantidad.ToString());
                                    AddCellToBody(tableLayout, entrada.Fecha.ToShortDateString());
                                }
                            }
                        }
                    }
                }
            }
            
            tableLayout.AddCell(
                new PdfPCell(new Phrase("\n\n\n\n\n________________________", new Font(Font.FontFamily.HELVETICA, 13, 1)))
                {
                    Colspan = 15,
                    Border = 0,
                    PaddingBottom = 0,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            tableLayout.AddCell(
                new PdfPCell(new Phrase("Firma de enterado", new Font(Font.FontFamily.HELVETICA, 13, 1)))
                {
                    Colspan = 18,
                    Border = 0,
                    PaddingBottom = 20,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            return tableLayout;
        }

        [Authorize(Roles = "PM, Admin")]
        // Method to add single cell to the header  
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });
        }

        [Authorize(Roles = "PM, Admin")]
        // Method to add single cell to the body  
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });
        }

    }
}