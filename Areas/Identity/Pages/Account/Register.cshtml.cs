using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using AWGestion.Areas.Identity.Data;

namespace AWGestion.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AWGestionUsers> _signInManager;
        private readonly UserManager<AWGestionUsers> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        //private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<AWGestionUsers> userManager,
            SignInManager<AWGestionUsers> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
            //RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            //_roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nombre")]
            public string Nombres { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Apellido Paterno")]
            public string ApPat { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Apellido Materno")]
            public string ApMat { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nombre de usuario")]
            public string UserName { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Registro Federal de Contribuyente (RFC)")]
            public string RFC { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Repita la contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña no coincide!")]
            public string ConfirmPassword { get; set; }

            //public string Name { get; set; }
        }

        //public void OnGet(string returnUrl = null)
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //ViewData["roles"] = _roleManager.Roles.ToList();
            //ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            //var role = _roleManager.FindByNameAsync(Input.Name).Result;
            if (ModelState.IsValid)
            {
                var user = new AWGestionUsers { UserName = Input.UserName, RFC = Input.RFC , Nombres = Input.Nombres, ApPat = Input.ApPat, ApMat = Input.ApMat};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    //await _userManager.AddToRoleAsync(user, role.Name);

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.RFC, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                    //Para añadir rol
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
