using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DataModels;
using Models.ViewModels;
using Repository;
using ToolBox.Mappers;

namespace ISIPS_WebApp01.Controllers
{
    public class UserInfoController : Controller
    {
        IRepository<User> users = new UserRepository();
        public IActionResult AllUsers()
        {
            string sessionStatus = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
            if (sessionStatus == (Role.Admin).ToString())
            {
                List<User> allusers = users.Select().ToList();
                return View(allusers);
            }
            return Unauthorized();
            
        }

        [HttpPost]
        public IActionResult addNewUser(AddNewUserViewModel addedUser)
        {
            string sessionStatus = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
            if (sessionStatus == (Role.Admin).ToString())
            {
                users.Insert(addedUser.User);
                return RedirectToAction("Index", "Home");
            }
            return Unauthorized();   
        }

        public IActionResult UserEdit(int id)
        {
            string sessionStatus = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
            if (sessionStatus == (Role.Admin).ToString())
            {
                User u = users.Select(id);
                AddNewUserViewModel vm = new AddNewUserViewModel();
                vm.User = u;
                return View(vm);
            }
            return Unauthorized();
        }

        public IActionResult GetUserDetails(int id)
        {
            string sessionStatus = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
            if (sessionStatus == (Role.Admin).ToString())
            {
                User u = users.Select(id);
                return View(u);
            }
            return Unauthorized();
        }

        [HttpPost]
        public IActionResult UpdateUserDetails(AddNewUserViewModel userDetails)
        {
            string sessionStatus = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
            if (sessionStatus == (Role.Admin).ToString())
            {
                users.Update(userDetails.User);
                return RedirectToAction(nameof(AllUsers));
            }
            return Unauthorized();
        }

        [ActionName("DeleteUser")]
        public void DeleteUser(int id)
        {
            users.Delete(id);
        }
    }
}