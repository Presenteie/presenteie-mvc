using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presenteie.Migrations;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    /// [Authorize]
    public class ItemsListController : Controller
    {
        private readonly PresenteieContext _context;

        public ItemsListController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }
        
        // GET
        [HttpGet]
        public IActionResult Index([FromForm] long idList)
        {
            idList = 1;
            var items = _context.Lists.Join(
                _context.Items,
                list => list.Id,
                item => item.IdList,
                (list, item) => item
            ).ToList();
            
            var list = _context.Lists.Where(list1 => list1.Id == idList).First();

            ViewBag.UserName = User.Identity.Name;
            ViewBag.UserId = long.Parse(User.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value);
            ViewBag.Items = items;
            ViewBag.List = list;
            return View();
        }

        [HttpPost("{idItem}")]
        public IActionResult Delete(long idItem)
        {
            var items = _context.Lists.Join(
                _context.Items,
                list => list.Id,
                item => item.IdList,
                (list, item) => item
            ).ToList();
            var item = items.Where(item => item.Id == idItem).First();
            
            Console.WriteLine(item.Id);
            _context.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}