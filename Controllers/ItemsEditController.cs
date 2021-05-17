using Microsoft.AspNetCore.Mvc;

namespace Presenteie.Controllers
{
    public class ItemsEditController : Controller
    {
        // GET
        public IActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View(message);
        }
    }
}