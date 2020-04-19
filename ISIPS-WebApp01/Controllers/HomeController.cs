using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels;
using Repository;

namespace ISIPS_WebApp01.Controllers
{
    public class HomeController : Controller
    {

        IRepository<User> users = new UserRepository();
        public IActionResult Index()
        {
            string tosendToView = HttpContext.Session.Keys.Count() > 0 ? HttpContext.Session.GetString("sessionFirstname") : "Vous n'êtes pas connecté";
            return View("Index", tosendToView);
        }

        public IActionResult LoginSuccess(dynamic name)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult UpdateUserProfile(User user)
        {
            User userDb = users.Update(user);
            ViewBag.message = "Profile mis à jour avec succès";
            return View("Profile", userDb);
        }

        public IActionResult EditProfile(int id)
        {
            User user = users.Select(id);
            return View(user);
        }

        public IActionResult Profile()
        {
            int? sessionId = HttpContext.Session.GetInt32("sessionId");
            if (!(sessionId is null))
            {
                User user = users.Select((int)sessionId);
                return View(user);
            }
            ViewBag.message = "Vous n'êtes pas connecté";
            return RedirectToAction("Login", "Identity");
            
        }
    }
}
