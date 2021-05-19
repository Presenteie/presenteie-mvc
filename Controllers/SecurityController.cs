using BC = BCrypt.Net.BCrypt;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Presenteie.Models;
using RestSharp;
using RestSharp.Authenticators;

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

        private void SendMail(string email, string hash)
        {
            var client = new RestClient("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", "b042cbb695f9785136b42e26b1bc0237-9b1bf5d3-dbb8c6dd");
            
            var request = new RestRequest();
            
            request.AddParameter("domain", "presenteie.ml", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Presente.ie <sistema@presenteie.ml>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Recuperação de Senha");
            request.AddParameter("template", "password");
            request.AddParameter("h:X-Mailgun-Variables", "{\"url\": \"https://localhost:5001/Security/" + hash + "\"}");
            request.Method = Method.POST;
            
            Console.WriteLine(client.Execute(request).Content.ToString());
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
                        ViewBag.hash = hash;
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
                    UserId = account.Id,
                    Hash = stringBuilder.ToString(),
                    ExpiresAt = DateTime.Now.AddDays(1),
                    CreatedAt = DateTime.Now
                };

                _context.Add(security);
                _context.SaveChanges();
                
                SendMail(account.Email, security.Hash);
            }
            
            return RedirectToAction("Index", "Security");
        }

        [HttpPost("Reset/{hash}")]
        public IActionResult Reset(string hash, [FromForm] string password)
        {
            var security = _context.Security.FirstOrDefault(sec => sec.Hash.Equals(hash));

            if (security != null)
            {
                if (DateTime.Compare(DateTime.Now, security.ExpiresAt) < 0)
                {
                    var user = _context.Users.FirstOrDefault(usr => usr.Id.Equals(security.UserId));

                    if (user != null)
                    {
                        user.Password = BC.HashPassword(password);
                    }   
                }

                _context.Security.Remove(security);
                _context.SaveChanges();
                
                return RedirectToAction("Index", "Login");
            }

            return NotFound();
        }
    }
}