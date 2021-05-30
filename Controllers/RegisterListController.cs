using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    public class RegisterListController : Controller
    {

        
        private readonly PresenteieContext _context;
        [BindProperty]
        public List list {get; set;}
        public RegisterListController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }

        //The database returns the event ID to identify each list created.
        [HttpGet ("List/Create")]
        public IActionResult Index()
        {
            var userId = long.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            ViewBag.UserId = userId;
            return View();
        }

        //The database returns the date of the event previously entered by the user.
        [HttpPost]
        public IActionResult Create([FromForm] List list)
        {
            Console.WriteLine(list.EventDate);
            list.CreatedDate = DateTime.Now;
            _context.Add(list);
            _context.SaveChanges();
            return RedirectToRoute(new{
                controller = "Home",
                action = "Index"
            });   
        }

        //The information subsequently inserted in a list is returned and shown to the user. 
        [HttpGet("List/Edit/{idList}")]
        public IActionResult Index([FromRoute] long idList)
        {
            var lists = _context.Users.Join(
                _context.Lists,
                user => user.Id,
                list => list.IdUser,
                (user, list) => list
            ).ToList();
            var userId = long.Parse(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            list = lists.Where(list => list.Id == idList && list.IdUser == userId).FirstOrDefault();
            if (list != null) {
                
                Console.WriteLine(list.Description);
                ViewBag.List = list;
                return View("Edit");
            }

            
            return RedirectToRoute(new {

                controller = "Home",
                action = "Index"
            });
        }
        //After inserting all the information in the list, the user has the possibility to edit them, thus updating all the data in the database. 
        [HttpPost("List/Edit")]
        public IActionResult Edit([FromForm] List list)
    
        {

            _context.Entry(list).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToRoute(new {

                controller = "Home",
                action = "Index"
            });
        }
        
        
        //The user can delete list and all information contained in a list.
        [HttpGet("List/Delete/{idList}")]
        public IActionResult Delete([FromRoute] long idList)
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

            var list = lists.Where(list1 => list1.Id == idList && list1.IdUser == UserId).FirstOrDefault();

            var itemsVerify = items.Where(item => item.IdList == idList).ToList();

            if (list != null)
            {
                foreach(var item in itemsVerify) {
                    _context.Remove(item);
                }

                _context.Remove(list);
                _context.SaveChanges();
                

                return RedirectToRoute(new
                {
                    controller = "Home",
                    action = "Index"
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