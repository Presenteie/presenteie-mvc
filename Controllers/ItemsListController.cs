using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    public class ItemsListController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] string message)
        {
            Console.WriteLine(message);
            // TODO preciso achar um jeito de enviar a variavel item para a view
            return RedirectToAction("Index", "ItemsEdit", message);
        }
    }
}