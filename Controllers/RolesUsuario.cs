using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Security;
using AWGestion.Data;
using AWGestion.Models;
using AWGestion.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace AWGestion.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesUsuario : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AWGestionUsers> userManager;

        public RolesUsuario(RoleManager<IdentityRole> roleManager, UserManager<AWGestionUsers> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Roles()
        {
            var roles = roleManager.Roles;
            RegistraBitacora("AspNetRoles", "Consulta");
            return View(roles);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CrearRol()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task <IActionResult> CrearRol(AspNetRole model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.Name
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    RegistraBitacora("AspNetRoles", "Inserción");
                    TempData["RolCreado"] = $"Los roles han sido modificados con exito!\nPor favor reinicia la sesión del usuario en cuestión para aplicar los ajustes.";
                    return RedirectToAction("Roles", "RolesUsuario");                                        
                }

                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AgregarUsuario(string roleId)
        {
            //ViewBag.roleId = roleId;
            //ViewBag.roleName = roleManager.FindByIdAsync(roleId);

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                TempData["NoRol"] = $"No se encontró el Rol especificado";
                return View("Roles");
            }
            else
            {
                ViewBag.roleName = role.Name;

                var model = new List<AspNetUserRole>();

                foreach(var user in userManager.Users)
                {
                    var rol = new AspNetUserRole
                    {
                        UserId = user.Id,
                        UserName = user.UserName
                    };

                    if (await userManager.IsInRoleAsync(user, role.Name))
                    {
                        rol.IsSelected = true;
                    }
                    else
                    {
                        rol.IsSelected = false;
                    }
                    model.Add(rol);
                    RegistraBitacora("AspNetRoles", "Consulta");
                }
                
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AgregarUsuario(List<AspNetUserRole> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                TempData["NoRol"] = $"No se encontró el Rol especificado";
                return View("Roles");
            }

            int count = 0;
            
            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                    count++;
                }
                else if(!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    count++;
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    // count++;
                    RegistraBitacora("AspNetUserRoles", "Inserción");
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    TempData["RolModificado"] = $"Los roles han sido modificados con exito!\nPor favor reinicia la sesión del usuario en cuestión para aplicar los ajustes.";
                    return RedirectToAction("Roles", "RolesUsuario");
                }
            }
            
            if (count > 0)
                TempData["RolModificado"] = $"Los roles han sido modificados con exito!\nPor favor reinicia la sesión del usuario en cuestión para aplicar los ajustes.";
            return RedirectToAction("Roles", "RolesUsuario");
        }

        [Authorize]
        public void ExecuteQuery(string query)
        {
            SqlConnection conection = new SqlConnection("Server= localhost; Database= webstore; Integrated Security=SSPI; Server=localhost\\sqlexpress;");
            conection.Open();
            SqlCommand command = new SqlCommand(query, conection); // Create a object of SqlCommand class
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