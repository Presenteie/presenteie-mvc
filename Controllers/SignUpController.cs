using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    public class SignUpController : Controller
    {
        private readonly PresenteieContext _context;
        
        public SignUpController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (_context.Users.FirstOrDefault(u => u.Email.Equals(user.Email)) is not null)
            {
                ModelState.AddModelError("Email", " The Email is already registered.");
                return View();
            }

            user.Password = BC.HashPassword(user.Password);
            _context.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        
    }
}