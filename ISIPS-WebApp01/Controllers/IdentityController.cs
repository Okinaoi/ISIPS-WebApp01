using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;
using Models.DTOs;
using Models.ViewModels;
using Repository;

namespace ISIPS_WebApp01.Controllers
{
    public class IdentityController : Controller
    {
        IRepository<User> userRepo = new UserRepository();

        [Route("/{controller}/")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewUser(RegisterViewModel u)
        {
            userRepo.Insert(u.User);
            return RedirectToAction(nameof(RegisterSuccess), routeValues: u.User.Firstname);
        }

        public IActionResult RegisterSuccess(string firstname)
        {
            return View(firstname);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login(LoginDto credentials)
        {
            
            SessionInfo sessionInfo = Services.CheckLoginCredentials(credentials);
            if (!(sessionInfo is null))
            {
                HttpContext.Session.SetInt32("sessionId", sessionInfo.UserId);
                HttpContext.Session.SetString("sessionFirstname", sessionInfo.Firstname);
                HttpContext.Session.SetInt32("sessionCompanyStatus", sessionInfo.Status);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction(nameof(Login));
        }


        [ActionName("CheckIdentity")]
        public int CheckNationalNumberNotDuplicated()
        {

            return 1; 
        }
    }
}