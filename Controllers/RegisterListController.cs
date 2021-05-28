using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;
using System;
using System.Security.Claims;

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
            var userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            //list.IdUser = int.Parse(userId);
            //_context.Lists.Add(list);
            return View();
        }
    }
}