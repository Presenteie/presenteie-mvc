using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;
using Presenteie.Models;

namespace Presenteie.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly PresenteieContext _context;

        public SettingsController(PresenteieContext presenteieContext)
        {
            _context = presenteieContext;
        }
        
        /// <summary>
        /// Method to get the logged user data.
        /// </summary>
        /// <returns>
        /// Returns IActionResult.
        /// </returns>
        [HttpGet]
        public IActionResult Index()
        {
            var userContext = HttpContext.User;

            if (userContext.Identity is {IsAuthenticated: false})
            {
                return RedirectToAction(actionName: "Index", controllerName: "Login");
            }

            var userId = long.Parse(userContext.Claims.First().Value);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
         
            return View(user);
        }

        /// <summary>
        /// Method that loads the ChangePassword page.
        /// </summary>
        /// <returns>
        /// Returns IActionResult.
        /// </returns>
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View("ChangePassword");
        }

        /// <summary>
        /// Method that changes and saves the logged user data.
        /// </summary>
        /// <param name="form"></param>
        /// <returns>
        /// Returns IActionResult.
        /// </returns>
        [HttpPost, ActionName("Edit")]
        public IActionResult Edit(User form)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == form.Id);
            
            if (user != null)
            {
                user.Name = form.Name;
                user.Email = form.Email;

                _context.Update(user);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Method that changes and saves the logged user password.
        /// </summary>
        /// <param name="form"></param>
        /// <returns>
        /// Returns IActionResult.
        /// </returns>
        [HttpPost, ActionName("UpdatePassword")]
        public IActionResult UpdatePassword(User form)
        {
            var userContext = HttpContext.User;

            if (userContext.Identity is {IsAuthenticated: false})
            {
                return RedirectToAction(actionName: "Index", controllerName: "Login");
            }
        
            var userId = int.Parse(userContext.Claims.First().Value);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Password = BC.HashPassword(form.Password);
                _context.Update(user);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Method that delete the user account. Must be implemented in the next sprint.
        /// </summary>
        /// <param name="form"></param>
        /// <returns>
        /// Returns IActionResult.
        /// </returns>
        [HttpDelete]
        public IActionResult Delete(User form)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == form.Id);
            if (user != null)
            {
                _context.Remove(user);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}