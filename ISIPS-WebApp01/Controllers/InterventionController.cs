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
    public class InterventionController : Controller
    {
        ISpecificRepository<Intervention> repo = new InterventionRepository();
        public IActionResult TechnicianInterventions()
        {
            if (HttpContext.Session.GetInt32("sessionCompanyStatus") != null)
            {
                Role sessionRole = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
                if (sessionRole == Role.Technician)
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
                Role sessionRole = ((int)HttpContext.Session.GetInt32("sessionCompanyStatus")).ToRole();
                if (sessionRole == Role.Admin)
                {
                    List<AdminInterventionViewModel> interList = repo.SelectForAdmin().ToList().ToListAdminInterventionVm().ToList();
                    return View(interList);
                }
                return Unauthorized();
            }
            return RedirectToAction("Index", "Home");

        }
    }
}