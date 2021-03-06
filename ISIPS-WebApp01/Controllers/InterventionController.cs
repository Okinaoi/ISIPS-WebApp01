﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        IRepository<Intervention> interventions = new InterventionRepository();
        IRepository<User> users = new UserRepository();
        IRepository<Contract> contracts = new ContractRepository();

        public IActionResult AdminDetails(int id)
        {
            Intervention intervention = interventions.Select(id);
            AdminInterventionViewModel vm = new AdminInterventionViewModel(intervention);
            return View(vm);
        }

        public IActionResult InterventionsFromContractForAdmin(int id)
        {
            List<AdminInterventionViewModel> vm = Services.GetInterventionsFromContractId(id).ToList().ToListAdminInterventionVm().ToList();
            return View(vm);
        }

        public IActionResult InterventionsFromContractForClient(int id)
        {
            List<AdminInterventionViewModel> vm = Services.GetInterventionsFromContractId(id).ToList().ToListAdminInterventionVm().ToList();
            return View(vm);
        }

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

        public IActionResult AddInterventionToContract(int contractId)
        {
            Intervention inter = new Intervention(contractId);
            Contract contr = contracts.Select(contractId);
            inter.InterventionAddress = Services.GetAdressFromContract(contractId);
            inter.Client = users.Select(contr.Client.UserId);
            AdminInterventionViewModel interVm = new AdminInterventionViewModel(inter);
            interVm.ContractDescription = contr.Description;
            interVm.ContractId = contr.ContractId;
            return View("addNewIntervention", interVm);
        }

        public IActionResult AdminUpdateContractAddIntervention(int contractId)
        {
            Intervention inter = new Intervention(contractId);
            Contract contr = contracts.Select(contractId);
            inter.InterventionAddress = Services.GetAdressFromContract(contractId);
            inter.Client = users.Select(contr.Client.UserId);
            AdminInterventionViewModel interVm = new AdminInterventionViewModel(inter);
            interVm.ContractDescription = contr.Description;
            interVm.ContractId = contr.ContractId;
            interventions.Insert(interVm.GetIntervention);
            return RedirectToAction(nameof(InterventionsFromContractForAdmin), new { id = contractId});
        }

        public IActionResult addNewIntervention()
        {
            return View();
        }

        [HttpPost]
        public IActionResult addNewIntervention(AdminInterventionViewModel intervention)
        {
            interventions.Insert(intervention.GetIntervention);
            return RedirectToAction(nameof(InterventionsFromContractForAdmin), new { id = intervention.ContractId });
            //ViewBag.message = $"Une nouvelle intervention à bien été ajoutée au contrat (id: {intervention.ContractId}) pour le client : {intervention.CustomerName} (id: {intervention.CustomerId})";
            //return View("~/Views/Contract/OnGoingContracts.cshtml");
        }

        public IActionResult Edit(int id)
        {
            Intervention inter = interventions.Select(id); // va chercher l'objet en db
            AdminInterventionViewModel vm = new AdminInterventionViewModel(inter);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(AdminInterventionViewModel vm)
        {
            Intervention inter = vm.GetIntervention;
            interventions.Update(inter);
            return RedirectToAction(nameof(TechnicianInterventions));
        }

        [HttpPost]
        public IActionResult AdminEdit(AdminInterventionViewModel vm)
        {
            Intervention inter = vm.GetIntervention;
            interventions.Update(inter);
            return RedirectToAction(nameof(InterventionsFromContractForAdmin), new { id = inter.ContractId });
        }

        public IActionResult Details(int id)
        {
            Intervention inter = interventions.Select(id); // va chercher l'objet en db
            AdminInterventionViewModel vm = new AdminInterventionViewModel(inter); // met l'objet dans mon view model
            return View(vm); // creer une vue dynamiquement grace a l'objet qu'on injecte dedan
        }

        public IActionResult ClientDetails(int id)
        {
            Intervention inter = interventions.Select(id); // va chercher l'objet en db
            AdminInterventionViewModel vm = new AdminInterventionViewModel(inter); // met l'objet dans mon view model
            return View(vm); // creer une vue dynamiquement grace a l'objet qu'on injecte dedan
        }

    }
}