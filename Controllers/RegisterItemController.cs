using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("Item/Create/{idList}")]
        public IActionResult Index([FromRoute] long idList)
        {
            Console.WriteLine(idList);
            var lists = _context.Users.Join(
                _context.Lists,
                user => user.Id,
                list => list.IdUser,
                (user, list) => list
            ).ToList();

            var UserId = long.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            var list = lists.Where(list1 => list1.Id == idList && list1.IdUser == UserId).FirstOrDefault();

            if (list != null)
            {
                ViewBag.IdList = list.Id;
                return View();
            }
            var id = idList;
            return RedirectToRoute(new
            {
                controller = "ItemsList",
                action = "Index",
                idList = id
            });
        }

        [HttpPost]
        public IActionResult Create([FromForm] Item item)
        {
            _context.Add(item);
            _context.SaveChanges();

            return RedirectToRoute(new
            {
                controller = "ItemsList",
                action = "Index",
                idList = item.IdList
            });
        }
        
        [HttpPost]
        public IActionResult SaveEdit([FromForm] Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToRoute(new
            {
                controller = "ItemsList",
                action = "Index",
                idList = item.IdList
            });
        } 
        
        [HttpGet("Item/Edit/{idItem}")]
        public IActionResult Edit([FromRoute] long idItem)
        {
            var lists = _context.Users.Join(
                _context.Lists,
                user => user.Id,
                list => list.IdUser,
                (user, list) => list
            ).ToList();
            
            var items = _context.Lists.Join(
                _context.Items,
                list => list.Id,
                item => item.IdList,
                (list, item) => item
            ).ToList();
            
            var UserId = long.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            var item = items.Where(item1 => item1.Id == idItem).FirstOrDefault();
            var list = lists.Where(list1 => list1.Id == item.IdList && list1.IdUser == UserId).FirstOrDefault();

            if (list != null)
            {
                ViewBag.Item = item;
                return View("Edit"); 
            }
            
            return RedirectToRoute(new
            {
                controller = "ItemsList",
                action = "Index",
                idList = item.IdList
            });
        }
        
        [HttpGet("Item/Delete/{idItem}")]
        public IActionResult Delete([FromRoute] long idItem)
        {
            var lists = _context.Users.Join(
                _context.Lists,
                user => user.Id,
                list => list.IdUser,
                (user, list) => list
            ).ToList();
           
            var items = _context.Lists.Join(
                _context.Items,
                list => list.Id,
                item => item.IdList,
                (list, item) => item
            ).ToList();

            var item = items.Where(item => item.Id == idItem).FirstOrDefault();

            if (item != null)
            {
                var list = lists.Where(list => list.Id == item.IdList).FirstOrDefault();
                var UserId = long.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);


                if (list.IdUser == UserId)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }

                return RedirectToRoute(new
                {
                    controller = "ItemsList",
                    action = "Index",
                    idList = list.Id
                });
            }

            return RedirectToRoute(new
            {
                controller = "Home",
                action = "index"
            });
        }
    }
    
    

}