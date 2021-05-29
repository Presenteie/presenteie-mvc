using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presenteie.Controllers
{ 
    [Authorize]
    public class ItemsListController : Controller
    {
        private readonly PresenteieContext _context;

        public ItemsListController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }

        [HttpGet("ItemsList/{idList}")]
        public IActionResult Index(long idList)
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

            if (list != null)
            {
                items = items.Where(item => item.IdList == list.Id).ToList();
                if (list.IdUser == UserId)
                {
                    ViewBag.UserName = User.Identity.Name;
                    ViewBag.Items = items;
                    ViewBag.List = list;
                    return View();
                }
            }
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index"
            });
        }
    }
}