using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;
using System;
using System.Security.Claims;
using System.Linq;

namespace Presenteie.Controllers
{
    public class RegisterListController : Controller
    {
        private readonly PresenteieContext _context;

        public RegisterListController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] List list)
        {
            var userId = long.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            //_context.Lists.Add(list);
            //return View();
            return RedirectToAction("Index", "Home");   
        }


    }
}