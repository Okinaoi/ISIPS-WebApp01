using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;
using Repository;

namespace ISIPS_WebApp01.Controllers
{
    public class InterventionController : Controller
    {
        ISpecificRepository<Intervention> repo = new InterventionRepository();
        public IActionResult Index()
        {
            List<Intervention> interList = repo.SelectByTechnician(4).ToList();
            return View();
        }
    }
}