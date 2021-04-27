using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml;
using Microsoft.Data.SqlClient;
using AWGestion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AWGestion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MarcaController : Controller
    {
        private GestionContext _context;
        
        public MarcaController(GestionContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Marcas()
        {
            RegistraBitacora("Marcas", "Consulta");
            return View(_context.Marcas.ToList());
        }

        [Authorize(Roles = "Admin")]
        [Route("Marca/BorrarMarca/{marcaId}")]
        public IActionResult BorrarMarca(int marcaId)
        {
            if (marcaId != 0)
            {
                if (_context.Marcas.Find(marcaId) != null)
                {
                    _context.Marcas.Remove(_context.Marcas.Find(marcaId));
                    _context.SaveChanges();
                }
            }  
            RegistraBitacora("Marcas", "Borrado");
            return View("Marcas",_context.Marcas.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CrearMarca()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearMarca(Marca marca)
        {
            marca.VcMarcaStatus = "AC";

            if (ModelState.IsValid)
            {              
                _context.Marcas.Add(marca);
                _context.SaveChanges();
                RegistraBitacora("Marcas", "Inserción");
                return View("Marcas", _context.Marcas.ToList());
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("Marca/EditarMarca/{marcaId}")]
        public IActionResult EditarMarca(int marcaId)
        {
            Marca marca = _context.Marcas.Find(marcaId);
            return View(marca);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Marca/EditarMarca/{marcaId}")]
        public IActionResult EditarMarca(Marca marca)
        {
            if (ModelState.IsValid)
            {
                _context.Marcas.Update(marca);
                _context.SaveChanges();
                RegistraBitacora("Marcas", "Edición");
                return View("Marcas", _context.Marcas.ToList());
            }

            return View("EditarMarca", marca);
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
    }
}