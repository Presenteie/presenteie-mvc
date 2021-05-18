using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    [Route("Security")]
    public class SecurityController : Controller
    {
        private readonly PresenteieContext _context;

        public SecurityController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }
        
        [HttpGet("{hash?}")]
        public IActionResult Index(string hash)
        {
            if (hash != null)
            {
                var security = _context.Security.FirstOrDefault(sec => sec.Hash.Equals(hash));
        
                if (security != null)
                {
                    if (DateTime.Compare(DateTime.Now, security.ExpiresAt) < 0)
                    {
                        return View("Reset");
                    }
        
                    _context.Security.Remove(security);
                    _context.SaveChanges();
                }
        
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Save([FromForm] string email)
        {
            var account = _context.Users.FirstOrDefault(user => user.Email.Equals(email));

            if (account != null)
            {
                var md5 = MD5.Create();
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(account.Email));
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var t in data)
                {
                    stringBuilder.Append(t.ToString("x2"));
                }

                var security = new Security()
                {
                    IdUser = account.Id,
                    Hash = stringBuilder.ToString(),
                    ExpiresAt = DateTime.Now.AddDays(1),
                    CreatedAt = DateTime.Now
                };

                _context.Add(security);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index", "Security");
        }
    }
}