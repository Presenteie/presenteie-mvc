using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    public class RegisterItemController : Controller
    {
        private readonly PresenteieContext _context;
        
        public RegisterItemController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }
        
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index([FromForm] Item item)
        {
            _context.Add(item);
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Home");
        }
    }
    
}