using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    public class PasswordController : Controller
    {
        private readonly PresenteieContext _context;

        public PasswordController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] string email)
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
            
            return RedirectToAction("Index", "Password");
        }

        [HttpGet("Password/Reset/{hash}")]
        public IActionResult Reset(string hash)
        {
            var userHash = _context.Security.FirstOrDefault(sec => sec.Hash.Equals(hash));

            if (userHash != null)
            {
                if (DateTime.Now.)
                {
                    
                }
                return View();   
            }

            return NotFound();
        }
    }
}