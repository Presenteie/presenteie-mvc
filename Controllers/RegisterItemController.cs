using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        
        [HttpGet("RegisterItem/{idList}")]
        public IActionResult Index([FromRoute] long idList)
        {
            ViewBag.IdList = idList;
            return View();
        }
        
        [HttpPost]
        public IActionResult Index([FromForm] Item item)
        {
            var items = _context.Lists.Join(
                _context.Items,
                list => list.Id,
                item => item.IdList,
                (list, item) => item
            ).ToList();
            var itemVerify = items.Where(item1 => item1.Id == item.Id).FirstOrDefault();
            
            if (itemVerify == null) 
            {
                _context.Add(item);
            }
            else
            {
                _context.Items.Update(itemVerify);
                
            }
            _context.SaveChanges();

            return RedirectToRoute(new
            {
                controller = "ItemsList",
                action = "Index",
                idList = item.IdList
            });
        }
        
        [HttpPost("RegisterItem/Edit/{idItem}")]
        public  IActionResult Edit([FromRoute] long idItem)
        {
           var items = _context.Lists.Join(
                           _context.Items,
                           list => list.Id,
                           item => item.IdList,
                           (list, item) => item
                       ).ToList();
                       
           var item = items.Where(item1 => item1.Id == idItem).FirstOrDefault();
	   
	   if(item != null) {
		   ViewBag.Item = item;
		   return RedirectToRoute(new
		   {
		       controller = "RegisterItem",
		       action = "Index",
		       idList = item.IdList
		   });
           }
           return RedirectToRoute( new 
               {
           	controller = "Home",
           	action = "Index"
            });
        }
        
    }
    
}