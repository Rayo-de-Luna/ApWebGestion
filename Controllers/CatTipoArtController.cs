using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using AWGestion.Models;

namespace AWGestion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CatTipoArtController: Controller
    {
        private GestionContext _context;
        
        public CatTipoArtController(GestionContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CatTipoArts()
        {
            ViewBag.Context = _context;
            RegistraBitacora("CatTipoArts", "Consulta");
            return View(_context.CatTipoArts.ToList());
        }

        [Authorize(Roles = "Admin")]
        [Route("CatTipoArt/EliminarTipoArt/{tipoArtId}")]
        public IActionResult EliminarCatTipoArt(int tipoArtId)
        {
            if (tipoArtId != 0)
            {
                if (_context.CatTipoArts.Find(tipoArtId) != null)
                {
                    _context.CatTipoArts.Remove(_context.CatTipoArts.Find(tipoArtId));
                    _context.SaveChanges();
                }
            }
            RegistraBitacora("CatTipoArts", "Borrado");
            return View("CatTipoArts", _context.CatTipoArts.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CrearTipoArt()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearTipoArt(CatTipoArt tipoArt)
        {
            if (ModelState.IsValid)
            {
                _context.CatTipoArts.Add(tipoArt);
                _context.SaveChanges();
                RegistraBitacora("CatTipoArts", "Inserción");
                return View("CatTipoArts", _context.CatTipoArts.ToList());
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("CatTipoArt/EditarTipoArt/{tipoArtId}")]
        public IActionResult EditarTipoArt(int tipoArtId)
        {
            CatTipoArt tipoArt = _context.CatTipoArts.Find(tipoArtId);
            return View(tipoArt);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CatTipoArt/EditarCatTipoArt/{tipoArtId}")]
        public IActionResult EditarCatTipoArt(CatTipoArt tipoArt)
        {
            if (ModelState.IsValid)
            {
                _context.CatTipoArts.Update(tipoArt);
                _context.SaveChanges();
                RegistraBitacora("CatTipoArts", "Edición");
                return View("CatTipoArts", _context.CatTipoArts.ToList());
            }

            return View("CatTipoArts", _context.CatTipoArts.ToList());;
        }
        
        [Authorize]
        public void ExecuteQuery(string query)
        {
            SqlConnection conection = new SqlConnection("Server= localhost; Database= webstore; Integrated Security=SSPI; Server=localhost\\sqlexpress;");
            conection.Open();
            SqlCommand command = new SqlCommand(query,conection); // Create a object of SqlCommand class
            command.ExecuteNonQuery(); conection.Close();
        }
        
        [Authorize]
        public void RegistraBitacora(string tabla, string operacion)
        {
            ExecuteQuery($"exec RegistraBitacora {tabla}, {operacion}");
        }
        
    }
}