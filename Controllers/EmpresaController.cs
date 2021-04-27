using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using AWGestion.Models;

namespace AWGestion.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EmpresaController : Controller
    {
        private GestionContext _context;
        
        public EmpresaController(GestionContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Empresas()
        {
            RegistraBitacora("Empresas", "Consulta");
            return View(_context.Empresas.ToList());
        }

        [Authorize(Roles = "Admin")]
        [Route("Empresa/EliminarEmpresa/{empresaRfc}")]
        public IActionResult EliminarEmpresa(string empresaRfc)
        {
            if (!string.IsNullOrEmpty(empresaRfc))
            {
                if (_context.Empresas.Find(empresaRfc) != null)
                {
                    _context.Empresas.Remove(_context.Empresas.Find(empresaRfc));
                    _context.SaveChanges();
                }
            }
            RegistraBitacora("Empresas", "Borrado");
            return View("Empresas", _context.Empresas.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CrearEmpresa()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CrearEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                if (_context.Empresas.Find(empresa.EmpresaRfc) != null)
                {
                    ModelState.AddModelError(string.Empty, "El RFC ingresado ya existe en el sistema");
                    return View("CrearEmpresa");
                }
                _context.Empresas.Add(empresa);
                _context.SaveChanges();
                RegistraBitacora("Empresas", "Inserción");
                return View("Empresas", _context.Empresas.ToList());
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("Empresa/EditarEmpresa/{empresaRfc}")]
        public IActionResult EditarEmpresa(string empresaRfc)
        {
            Empresa empresa = _context.Empresas.Find(empresaRfc);
            return View(empresa);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Empresa/EditarEmpresa/{empresaRfc}")]
        public IActionResult EditarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Empresas.Update(empresa);
                _context.SaveChanges();
                RegistraBitacora("Empresas", "Edición");
                return View("Empresas", _context.Empresas.ToList());
            }

            return View("EditarEmpresa", empresa);
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