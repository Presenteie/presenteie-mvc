using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    public class LoginController : Controller
    {
        private readonly PresenteieContext _context;
        
        public LoginController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }
        
        [HttpGet]
        public IActionResult Index(string returnUrl = "")
        {
            var model = new UserCredentials() { ReturnUrl = returnUrl }; 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] UserCredentials credentials)
        {
            if (!ModelState.IsValid)
                return View(credentials);
            
            var user = _context.Users.FirstOrDefault(u => u.Email.Equals(credentials.Email));

            if (user is null)
            {
                ModelState.AddModelError("Email", "Email não encontrado");
                return View();
            }

            if (BC.Verify(credentials.Password, user.Password) is false)
            {
                ModelState.AddModelError("Password", "Senha incorreta");
                return View();
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Name),
            };
            
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = credentials.RememberMe
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            
            if (!string.IsNullOrEmpty(credentials.ReturnUrl) && Url.IsLocalUrl(credentials.ReturnUrl)) {
                return Redirect(credentials.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        
    }
}