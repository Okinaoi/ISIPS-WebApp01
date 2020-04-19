using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public class InterventionController : Controller
    {
        ISpecificRepository<Intervention> repo = new InterventionRepository();
        IRepository<User> users = new UserRepository();
        public IActionResult TechnicianInterventions()
        {
            if (HttpContext.Session.GetInt32("sessionCompanyStatus") != null)
            {
                string sessionRole = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
                if (sessionRole == (Role.Technician).ToString())
                {
                    int userId = (int)HttpContext.Session.GetInt32("sessionId");
                    List<TechnicianInterventionViewModel> interList = repo.SelectByTechnician(userId).ToList().ToListTechInterventionVm().ToList();
                    return View(interList);
                }
                return Unauthorized();
            }
            return RedirectToAction("Index", "Home");
            
        }

        public IActionResult AdminInterventions()
        {
            if (HttpContext.Session.GetInt32("sessionCompanyStatus") != null)
            {
                string sessionRole = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
                if (sessionRole == (Role.Admin).ToString())
                {
                    List<AdminInterventionViewModel> interList = repo.SelectForAdmin().ToList().ToListAdminInterventionVm().ToList();
                    return View(interList);
                }
                return Unauthorized();
            }
            return RedirectToAction("Index", "Home");

        }

        public IActionResult addNewIntervention()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addNewIntervention(AdminInterventionViewModel intervention)
        {
            return View();
        }

        [ActionName("GetTechInfo")]
        public async Task<IActionResult> GetTechInfo(int id)
        {
            User tech = users.Select(id);
            string Url = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            return Json(new { });
        }

        [ActionName("GetCustomerAddress")] 
        public async Task<IActionResult> GetCustomerAddress(int id)
        {

            User customer = users.Select(id);
            Address customerAddress = customer.PrivateAddress;
            return Json(customerAddress);
        }
    }
}