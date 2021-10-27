using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SitePet.Mvc.Interfaces;
using SitePet.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SitePet.Mvc.Controllers
{
    public class IdentidadeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<IdentidadeController> _logger;
        private readonly IPetService _petService;
        

        public IdentidadeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<IdentidadeController> logger, IPetService petService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _petService = petService;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return View(usuarioRegistro);

            var resposta = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true

            };

            var result = await _userManager.CreateAsync(resposta, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Identidade");
            }else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }

                return View(usuarioRegistro);
            }

           
          
           
               
                
        

            
      


        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioLogin usuarioLogin, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) return View(usuarioLogin);

            var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email,
             usuarioLogin.Senha, isPersistent: false, lockoutOnFailure: false);
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, usuarioLogin.Email),
                new Claim(ClaimTypes.Role, "Usuario_Comum")
            };
            var identidadeDeUsuario = new ClaimsIdentity(claims, "Login");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);


            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var Autenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddHours(2),
                IsPersistent = false
            };


            if (result.Succeeded)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, Autenticacao);
                _logger.LogInformation("Usuario Logado");

                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Pets");
                else
                {
                    return LocalRedirect(returnUrl);
                }


            }

            ModelState.AddModelError(string.Empty, "Email ou senha invalidos !!");
            return View(usuarioLogin);
        }

        [HttpGet]
        [Route("sair")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Pets");
        }


        [HttpGet]
        [Route("meu-perfil")]
        public async Task<IActionResult> Perfil(string email)
        {
            email = User.Identity.Name;
            var pet = await _petService.MostrarPetUsuario(email);
            return View(pet);
        }

      
    }
}
