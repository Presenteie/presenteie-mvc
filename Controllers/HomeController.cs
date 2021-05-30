using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly PresenteieContext _context;

        public HomeController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = long.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            
            var lists = _context.Lists.Where(list => list.IdUser == userId).Join(
                _context.Users,
                list => list.IdUser,
                user => user.Id,
                (list, user) => list
            ).ToList();

            ViewBag.lists = lists;
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}