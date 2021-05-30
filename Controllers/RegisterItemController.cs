using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    [Authorize]
    public class RegisterItemController : Controller
    {
        private readonly PresenteieContext _context;

        public RegisterItemController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }

        //After entering the item information, the user creates an item within the list.

        [HttpGet("Item/{idList}/Create")]
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
        //The view return the item previously entered by the user and save on database.
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
        //After inserting all the item information, the user can edit them, updating all data in the database.
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
        //Receive the item id, get item of database and send to view.

        
        [HttpGet("Item/{idItem}/Edit")]
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

        //The user can delete the item stored in the list
        
        [HttpGet("Item/{idItem}/Delete")]
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