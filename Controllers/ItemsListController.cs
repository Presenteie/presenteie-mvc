using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    /// [Authorize]
    public class ItemsListController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}